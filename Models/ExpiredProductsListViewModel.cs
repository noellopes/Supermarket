namespace Supermarket.Models
{
    public class ExpiredProductsListViewModel
    {
        public IEnumerable<ExpiredProducts> ExpiredProducts { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; } = string.Empty;
        public string SearchBarCode { get; set; } = string.Empty;
        public string SearchSupplier { get; set; } = string.Empty;
        public string SearchEmployee { get; set; } = string.Empty;
    }
}
