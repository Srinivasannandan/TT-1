using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _repository;

        public GetProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Product GetProduct(Guid id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _repository.GetAll();
        }
    }
}
