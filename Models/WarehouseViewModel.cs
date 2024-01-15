namespace Supermarket.Models
{
    public class WarehouseViewModel
    {
        public List<Warehouse> Warehouse { get; set; }
        public PagingInfoProduct PagingInfoProduct { get; set; }
        public string SearchName { get; set; }
        public string SearchAdress { get; set; }


    }
}
