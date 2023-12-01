using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(ProductId), nameof(ShelfId))]
    public class Shelft_ProductExhibition
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ShelfId { get; set; }
        public Shelf? Shelf { get; set; }

        [Range(0, 99999, ErrorMessage = "The quantity must be greater than zero")]
        public int Quantity { get; set; }

        [Range(1, 99999, ErrorMessage = "The minimum quantity must be greater than zero")]
        public int MinimumQuantity { get; set; }
    }
}
