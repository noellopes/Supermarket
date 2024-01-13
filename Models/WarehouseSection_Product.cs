using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(ProductId), nameof(WarehouseSectionId))]
    public class WarehouseSection_Product
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int WarehouseSectionId { get; set; }
        public WarehouseSection? WarehouseSection { get; set; }

        [Range(0, 99999, ErrorMessage = "The Quantity must be greater than zero")]
        public int Quantity { get; set; }

        [Range(0, 99999, ErrorMessage = "he Reserved Quantity must be greater than zero")]
        public int ReservedQuantity { get; set; } = 0;

    }
}
