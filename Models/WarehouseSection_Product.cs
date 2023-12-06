namespace Supermarket.Models
{
    public class WarehouseSection_Product
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int WarehouseSectionId { get; set; }
        public WarehouseSection? WarehouseSection { get; set; }

        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } = 0;
        public int MinimumQuantity { get; set; }
    }
}
