using System;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData
    {
        public OrderData(Order order)
        {
            Id = order.Id;
            ProductId = order.ProductId;
            CustomerName = order.CustomerName;
            Quantity = order.Quantity;
            UnitPrice = order.UnitPrice;
            Status = order.Status;
            CreatedUtc = order.CreatedUtc;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get { return Quantity * UnitPrice; } }
        public string Status { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}
