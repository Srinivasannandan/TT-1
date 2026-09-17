using System;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid productId, string customerName, int quantity,
            decimal unitPrice, string status)
        {
            order.SetProductId(productId);
            order.SetCustomerName(customerName);
            order.SetQuantity(quantity);
            order.SetUnitPrice(unitPrice);
            order.SetStatus(status);
        }
    }
}
