using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        required public string Name { get; set; }
       
    }
}
