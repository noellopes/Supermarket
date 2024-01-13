using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Hallway
    {
        public int HallwayId { get; set; }

        [Required(ErrorMessage = "Please enter a Hallway Description")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter your Hallway Description bigger than 3 leters")]
        public required string Description { get; set; }

        public int StoreId { get; set; }
        public Store? Store { get; set; }

    }
}
