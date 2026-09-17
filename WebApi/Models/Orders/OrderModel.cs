using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CustomerName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required(), Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than zero.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }
    }
}
