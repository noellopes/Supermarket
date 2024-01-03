using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class AvailabilitySupplier
    {

        [Key]
        public int SupplierAvailabilityID { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ProductQuantity { get; set; }
        public int DeliveryTime { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
    

