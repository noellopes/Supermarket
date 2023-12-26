using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models
{
    
    public class WarehouseSection_Product
    {
        [Key]
        public int WarehouseSection_ProductId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int WarehouseSectionId { get; set; }
        public WarehouseSection? WarehouseSection { get; set; }

        [Range(0, 99999, ErrorMessage = "The Quantity must be greater than zero")]
        public int Quantity { get; set; }

        [Range(0, 99999, ErrorMessage = "he Reserved Quantity must be greater than zero")]
        public int ReservedQuantity { get; set; } = 0;


        //Número de lote
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public required string BatchNumber { get; set; }

        //Data de validade
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        //Relation with foreign key for Supplier
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
