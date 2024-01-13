using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ProductDiscount
    {
        public int ProductDiscountId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
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
