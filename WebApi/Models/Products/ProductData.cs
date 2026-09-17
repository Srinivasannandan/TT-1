using System;
using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData
    {
        public ProductData(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            StockQuantity = product.StockQuantity;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
