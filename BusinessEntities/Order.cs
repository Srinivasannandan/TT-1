using System;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public Guid ProductId { get; private set; }
        public string CustomerName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public decimal Total => Quantity * UnitPrice;

        public void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("Product ID must not be empty.", nameof(productId));
            }
            ProductId = productId;
        }

        public void SetCustomerName(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentNullException(nameof(customerName));
            }
            CustomerName = customerName;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }
            Quantity = quantity;
        }

        public void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice));
            }
            UnitPrice = unitPrice;
        }

        public void SetStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentNullException(nameof(status));
            }
            Status = status;
        }

        public void SetCreatedUtc(DateTime createdUtc)
        {
            CreatedUtc = createdUtc;
        }
    }
}
