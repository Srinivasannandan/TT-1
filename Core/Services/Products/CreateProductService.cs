using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IIdObjectFactory<Product> _factory;
        private readonly IProductRepository _repository;
        private readonly IUpdateProductService _updateService;

        public CreateProductService(IIdObjectFactory<Product> factory,
            IProductRepository repository, IUpdateProductService updateService)
        {
            _factory = factory;
            _repository = repository;
            _updateService = updateService;
        }

        public Product Create(Guid id, string name, string description, decimal price, int stockQuantity)
        {
            if (_repository.Get(id) != null)
            {
                throw new InvalidOperationException("A product with this ID already exists.");
            }

            var product = _factory.Create(id);
            _updateService.Update(product, name, description, price, stockQuantity);
            _repository.Save(product);
            return product;
        }
    }
}
