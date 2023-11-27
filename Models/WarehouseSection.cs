using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class WarehouseSection
    {
        public int WarehouseSectionId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Description { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public ICollection<WarehouseSection_Product>? Products{ get; set; }
    }
}
