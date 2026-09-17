using BusinessEntities;
using Common;
using Data.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class OrderRepository : IOrderRepository
    {
        private readonly object _sync = new object();
        private readonly Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public OrderRepository()
        {
            AddDummy(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                new Guid("11111111-1111-1111-1111-111111111111"),
                "Alice Johnson", 1, 1299.99m, "Processing",
                new DateTime(2026, 9, 15, 14, 0, 0, DateTimeKind.Utc));
            AddDummy(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                new Guid("22222222-2222-2222-2222-222222222222"),
                "Robert Smith", 2, 39.95m, "Shipped",
                new DateTime(2026, 9, 16, 9, 30, 0, DateTimeKind.Utc));
        }

        public void Save(Order entity)
        {
            lock (_sync)
            {
                _orders[entity.Id] = entity;
            }
        }

        public void Delete(Order entity)
        {
            lock (_sync)
            {
                _orders.Remove(entity.Id);
            }
        }
        public void DeleteAll()
        {
            lock (_sync)
            {
                _orders.Clear();
            }
        }
        public Order Get(Guid id)
        {
            lock (_sync)
            {
                Order order;
                return _orders.TryGetValue(id, out order) ? order : null;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            lock (_sync)
            {
                return _orders.Values.ToList();
            }
        }

        private void AddDummy(Guid id, Guid productId, string customerName, int quantity,
            decimal unitPrice, string status, DateTime createdUtc)
        {
            var order = CreateWithId<Order>(id);
            order.SetProductId(productId);
            order.SetCustomerName(customerName);
            order.SetQuantity(quantity);
            order.SetUnitPrice(unitPrice);
            order.SetStatus(status);
            order.SetCreatedUtc(createdUtc);
            _orders.Add(id, order);
        }

        private static T CreateWithId<T>(Guid id) where T : IdObject, new()
        {
            var entity = new T();
            ReflectionHelper.SetProperty(entity, typeof(T).GetProperty("Id"), id);
            return entity;
        }
    }
}
