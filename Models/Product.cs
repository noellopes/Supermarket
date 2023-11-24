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

        public int TotalQuantity { get; set; }

        public int MinimumQuantity { get; set; }

        public double UnitPrice { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Status { get; set; } = "Unavailable";

        public ICollection<Shelft_ProductExhibition>? Shelf { get; set; }

        public ICollection<WarehouseSection_Product>? WarehouseSection { get; set; }




    }
}
