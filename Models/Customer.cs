using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        [Phone]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
