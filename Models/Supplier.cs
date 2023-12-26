using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
       
        public required string Name { get; set; }
    }
}
