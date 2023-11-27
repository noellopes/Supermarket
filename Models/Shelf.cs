using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Shelf
    {
        public int ShelfId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Name { get; set; }


        public int HallwayId { get; set; }
        public Hallway? Hallway { get; set; }

        public ICollection<Shelft_ProductExhibition>? Product { get; set; }
    }
}
