using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int BrandId { get; set; }

        public Brand? Brand { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Description { get; set; }

        [Range(0, 99999, ErrorMessage = "The Total Quantity must be greater than zero")]
        [Display(Name = "Total Quantity")]
        public int TotalQuantity { get; set; }

        [Range(0, 99999, ErrorMessage = "The Minimum Quantity must be greater than zero")]
        [Display(Name = "Minimum Quantity")]
        public int MinimumQuantity { get; set; }

        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        [Required]
        public required string Status { get; set; } = "Unavailable";

        public static List<string> StatusList = new List<string> { "Available", "Unavailable", "Discontinued" };

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Last Count Date")]
        public DateTime LastCountDate { get; set; } = DateTime.Now;

        public ICollection<Shelft_ProductExhibition>? Shelf { get; set; }

        public ICollection<WarehouseSection_Product>? WarehouseSection { get; set; }




    }
}
