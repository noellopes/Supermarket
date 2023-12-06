using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Shelf
    {
        public int ShelfId { get; set; }

        [Required(ErrorMessage = "Please enter a Shelf Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter your Shelf Name bigger than 3 leters")]
        public required string Name { get; set; }


        public int HallwayId { get; set; }
        public Hallway? Hallway { get; set; }

        public ICollection<Shelft_ProductExhibition>? Product { get; set; }
    }
}
