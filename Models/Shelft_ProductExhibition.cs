namespace Supermarket.Models
{
    public class Shelft_ProductExhibition
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ShelfId { get; set; }
        public Shelf? Shelf { get; set; }


        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
    }
}
