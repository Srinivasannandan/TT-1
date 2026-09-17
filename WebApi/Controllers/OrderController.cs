using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Orders;
using WebApi.App_Start;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [InMemoryData]
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService,
            IDeleteOrderService deleteOrderService, IGetOrderService getOrderService,
            IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var invalid = ValidateRequest(model);
            if (invalid != null) return invalid;
            if (_getOrderService.GetOrder(orderId) != null)
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "An order with this ID already exists.");

            var order = _createOrderService.Create(orderId, model.ProductId,
                model.CustomerName, model.Quantity, model.UnitPrice, model.Status);
            return Request.CreateResponse(HttpStatusCode.Created, new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var invalid = ValidateRequest(model);
            if (invalid != null) return invalid;
            var order = _getOrderService.GetOrder(orderId);
            if (order == null) return DoesNotExist();

            _updateOrderService.Update(order, model.ProductId, model.CustomerName,
                model.Quantity, model.UnitPrice, model.Status);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null) return DoesNotExist();
            _deleteOrderService.Delete(order);
            return Found();
        }
        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
           //Remoe data in collection
           _deleteOrderService.DeleteAll();
            return Found();
        }
        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            return order == null ? DoesNotExist() : Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip = 0, int take = 50)
        {
            if (skip < 0 || take < 1 || take > 200)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "skip must be at least 0 and take must be between 1 and 200.");

            var orders = _getOrderService.GetOrders().OrderByDescending(order => order.CreatedUtc)
                .Skip(skip).Take(take).Select(order => new OrderData(order)).ToList();
            return Found(orders);
        }

        private HttpResponseMessage ValidateRequest(OrderModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request body is required.");
            if (model.ProductId == Guid.Empty)
                ModelState.AddModelError("productId", "Product ID must be a non-empty GUID.");
            return ModelState.IsValid ? null :
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
