using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _factory;
        private readonly IOrderRepository _repository;
        private readonly IUpdateOrderService _updateService;

        public CreateOrderService(IIdObjectFactory<Order> factory,
            IOrderRepository repository, IUpdateOrderService updateService)
        {
            _factory = factory;
            _repository = repository;
            _updateService = updateService;
        }

        public Order Create(Guid id, Guid productId, string customerName, int quantity,
            decimal unitPrice, string status)
        {
            if (_repository.Get(id) != null)
            {
                throw new InvalidOperationException("An order with this ID already exists.");
            }

            var order = _factory.Create(id);
            order.SetCreatedUtc(DateTime.UtcNow);
            _updateService.Update(order, productId, customerName, quantity, unitPrice, status);
            _repository.Save(order);
            return order;
        }
    }
}
