using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Store
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Adress { get; set; }

        public ICollection<Hallway>? Hallway { get; set; }
    }
}
