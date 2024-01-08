using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; } = String.Empty;

        public ICollection<Issues>? Issue { get; set; }
    }
}

