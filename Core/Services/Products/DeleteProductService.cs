using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _repository;

        public DeleteProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Product product)
        {
            _repository.Delete(product);
        }
        public void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}
