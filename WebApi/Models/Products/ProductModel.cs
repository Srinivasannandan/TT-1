using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(), Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [Required( ErrorMessage = "Stock must be greater than or equal to zero.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to zero.")]
        public int StockQuantity { get; set; }
    }
}
