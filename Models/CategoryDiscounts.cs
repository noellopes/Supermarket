using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Supermarket.Models
{
    public class CategoryDiscounts
    {
        public int CategoryDiscountsId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [DisplayName("ClientCard")]
        public int ClientCardId { get; set; }
        public ClientCard? ClientCard { get; set; }
        public float Value { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
