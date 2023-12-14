using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Please enter a Warehouse Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter your Warehouse Name bigger than 3 leters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Warehouse Adress")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter your Warehouse Adress bigger than 3 leters")]
        public required string Adress { get; set; }

    }
}
