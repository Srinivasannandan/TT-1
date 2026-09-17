using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessEntities;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email ID is Required"),EmailAddress()]
        public string Email { get; set; }
        public UserTypes Type { get; set; }
        [Required(),Range(0.01, double.MaxValue,ErrorMessage = "Annual salary must be greater than zero.")]
        public decimal? AnnualSalary { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}