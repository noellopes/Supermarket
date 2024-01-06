using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class TakeAwayProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Product Name length must be shorter than 100 character")]
        [MinLength(3,ErrorMessage = "Product Name length must be longer than 3 character")]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public TakeAwayCategory? Category { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int? QuantityReserved { get; set; }

        [Required]
        [Range(0.1,int.MaxValue,ErrorMessage = "Price must be between 0.1 and 2 billion")]
        public double Price { get; set; }

        [Required]
        [Range(1,30,ErrorMessage = "Estimated Preparation time must be between 1 and 30 minutes")]
        public int EstimatedPreparationTimeAsMinutes { get; set; }
        public List<Order> Order { get; set; }

    }
}
