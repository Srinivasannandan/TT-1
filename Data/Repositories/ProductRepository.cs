using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class ProductRepository : IProductRepository
    {
        private readonly object _sync = new object();
        private readonly Dictionary<Guid, Product> _products = new Dictionary<Guid, Product>();

        public ProductRepository()
        {
            AddDummy(new Guid("11111111-1111-1111-1111-111111111111"),
                "Laptop", "15-inch business laptop", 1299.99m, 12);
            AddDummy(new Guid("22222222-2222-2222-2222-222222222222"),
                "Wireless Mouse", "Ergonomic wireless mouse", 39.95m, 75);
        }

        public void Save(Product entity)
        {
            lock (_sync)
            {
                _products[entity.Id] = entity;
            }
        }

        public void Delete(Product entity)
        {
            lock (_sync)
            {
                _products.Remove(entity.Id);
            }
        }

        public Product Get(Guid id)
        {
            lock (_sync)
            {
                Product product;
                return _products.TryGetValue(id, out product) ? product : null;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            lock (_sync)
            {
                return _products.Values.ToList();
            }
        }
        public void DeleteAll()
        {
            lock (_sync)
            {
                _products.Clear();
            }
        }
        private void AddDummy(Guid id, string name, string description, decimal price, int stockQuantity)
        {
            var product = CreateWithId<Product>(id);
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetStockQuantity(stockQuantity);
            _products.Add(id, product);
        }

        private static T CreateWithId<T>(Guid id) where T : IdObject, new()
        {
            var entity = new T();
            ReflectionHelper.SetProperty(entity, typeof(T).GetProperty("Id"), id);
            return entity;
        }
    }
}
