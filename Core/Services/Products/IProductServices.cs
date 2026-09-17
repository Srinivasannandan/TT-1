using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Product Create(Guid id, string name, string description, decimal price, int stockQuantity);
    }

    public interface IGetProductService
    {
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
    }

    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description, decimal price, int stockQuantity);
    }

    public interface IDeleteProductService
    {
        void Delete(Product product);
        void DeleteAll();
    }
}
