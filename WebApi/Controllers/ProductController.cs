using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.App_Start;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [InMemoryData]
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService,
            IDeleteProductService deleteProductService, IGetProductService getProductService,
            IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var invalid = ValidateRequest(model);
            if (invalid != null) return invalid;
            if (_getProductService.GetProduct(productId) != null)
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A product with this ID already exists.");

            var product = _createProductService.Create(productId, model.Name,
                model.Description, model.Price, model.StockQuantity);
            return Request.CreateResponse(HttpStatusCode.Created, new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var invalid = ValidateRequest(model);
            if (invalid != null) return invalid;
            var product = _getProductService.GetProduct(productId);
            if (product == null) return DoesNotExist();

            _updateProductService.Update(product, model.Name, model.Description,
                model.Price, model.StockQuantity);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null) return DoesNotExist();
            _deleteProductService.Delete(product);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            return product == null ? DoesNotExist() : Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip = 0, int take = 50)
        {
            if (skip < 0 || take < 1 || take > 200)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "skip must be at least 0 and take must be between 1 and 200.");

            var products = _getProductService.GetProducts().OrderBy(product => product.Name)
                .Skip(skip).Take(take).Select(product => new ProductData(product)).ToList();
            return Found(products);
        }
        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            
            _deleteProductService.DeleteAll();
            return Found();
        }
        private HttpResponseMessage ValidateRequest(ProductModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request body is required.");
            return ModelState.IsValid ? null :
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
