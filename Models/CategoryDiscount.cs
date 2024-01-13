using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class CategoryDiscount
    {
        [Key]
        public int CategoryDiscountId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        public float Value { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
