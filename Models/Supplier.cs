using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; } =  String.Empty;

        public ICollection<Issues>? Issue { get; set; }
    }
}
