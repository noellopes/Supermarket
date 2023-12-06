using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ProductDiscount
    {
        public int ProductDiscountId { get; set; }
        ICollection<Product> ProductId { get; set; }
        //ICollection<Card> CardNumber { get; set; }
        public float Value { get; set; }
        [Required]
        public required DateTime StartDate { get; set; }
        [Required]
        public required DateTime EndDate { get; set; }
    }
}
    