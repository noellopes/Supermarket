namespace Supermarket.Models
{
    public class ProductsWarehouseViewModel
    {
        public List<WarehouseSection_Product> WarehouseSections { get; set; }
        public PagingInfoProduct PagingInfoProduct { get; set; }
        public string SearchProductProduct { get; set; }
        public string SearchProductBach { get; set; }

    }
}
