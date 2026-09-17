using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _repository;

        public GetOrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Order GetOrder(Guid id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _repository.GetAll();
        }
    }
}
