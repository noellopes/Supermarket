using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        //Relation with foreign key for Product
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        //Relation with foreign key for Supplier
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        [Required]
        public int DeliveredQuantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
    }
}
