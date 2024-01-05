namespace Supermarket.Models
{
    public class WarehouseViewModel
    {
        public class ProductsWarehouseViewModel
        {
            public List<Warehouse> Warehouse{ get; set; }
            public PagingInfoProduct PagingInfoProduct { get; set; }
            public string SearchProductProduct { get; set; }
            public string SearchProductBach { get; set; }

        }
    }
}
