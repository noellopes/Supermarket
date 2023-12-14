using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ProductExpiration
    {
        [Key]
        //Ou pegar da tabela de produtos, ou criar nessa classse mesmo
        public int BatchId { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 3)]
        public required string BatchNumber { get; set; }

        //Relation with foreign key for Brand
        //[Display(Name = "Brand")]
        //public int BrandId { get; set; }
        //public Brand? Brand { get; set; }

        //Relation with foreign key for Product
        //[Display(Name = "Product")]
        //public int ProductId { get; set; }
        //public Product? Product { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public required int Quantity { get; set; }

        //Relation with foreign key for Supplier
        //[Display(Name = "Supplier")]
        //public int SupplierID { get; set; }
        //public Supplier? Supplier { get; set; }

    }
}
