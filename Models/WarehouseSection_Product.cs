using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace Supermarket.Models
{
    public class WarehouseSection_Product
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int WarehouseSectionId { get; set; }
        public WarehouseSection? WarehouseSection { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The Quantity must be greater than zero")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "he Reserved Quantity must be greater than zero")]
        public int ReservedQuantity { get; set; } = 0;

    }
}
