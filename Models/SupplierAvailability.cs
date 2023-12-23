using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class SupplierAvailability
    {
        [Key]
        public int SupplierAvailabilityID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Name { get; set; }
        public Product Product { get; set; }   


        public double UnitPriceSupplier { get; set; }

        public int ProductQuantity { get; set; }

        public int DeliveryTime { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}

