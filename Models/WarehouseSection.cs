using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class WarehouseSection
    {
        public int WarehouseSectionId { get; set; }

        [Required(ErrorMessage = "Please enter a Warehouse Section Description")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter your Warehouse Section Description bigger than 3 leters")]
        public required string Description { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public ICollection<WarehouseSection_Product>? Products { get; set; }
    }
}
