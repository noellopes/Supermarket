namespace Supermarket.Models
{
    public class PurchaseListViewModel
    {
        public IEnumerable<Purchase> Purchase{ get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; } = string.Empty;
        public string SearchSupplier { get; set; } = string.Empty;
        //public string SearchDate { get; set; } = string.Empty;
    }
}
