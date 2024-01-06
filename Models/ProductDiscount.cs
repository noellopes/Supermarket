using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class ProductDiscount
    {
        public int ProductDiscountId { get; set; }
        public int ClientCardId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ClientCard? clientCard { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ClientCardId { get; set; }
        public ClientCard? ClientCard { get; set; }

        public float Value { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
    