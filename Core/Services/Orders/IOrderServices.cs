using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, Guid productId, string customerName, int quantity,
            decimal unitPrice, string status);
    }

    public interface IGetOrderService
    {
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrders();
    }

    public interface IUpdateOrderService
    {
        void Update(Order order, Guid productId, string customerName, int quantity,
            decimal unitPrice, string status);
    }

    public interface IDeleteOrderService
    {
        void Delete(Order order);
        void DeleteAll();
    }
}
