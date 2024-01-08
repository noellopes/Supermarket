namespace Supermarket.Models
{
    public class PurchaseListViewModel
    {
        public IEnumerable<Purchase> Purchase{ get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; } = string.Empty;

        public DateTime? SearchExpirationDate { get; set; } = null;
        public DateTime? SearchDeliveryDate { get; set; } = null;
        public string SearchSupplier { get; set; } = string.Empty;
        

    }
}
