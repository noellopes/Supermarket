using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Name { get; set; }
    }
}
