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
        [Display(Name = "Delivered Quantity")]
        public int DeliveredQuantity { get; set; }

        //Relation with foreign key for Employee
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public required string BatchNumber { get; set; }

        //Data de validade
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
    }
}
