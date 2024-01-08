namespace Supermarket.Models
{
    public class ExpiredProductsListViewModel
    {
        public IEnumerable<ExpiredProducts> ExpiredProducts { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; } = string.Empty;
        public string SearchBatchNumber { get; set; } = string.Empty;
        public DateTime? SearchExpirationDate { get; set; } = null;
        public string SearchSupplier { get; set; } = string.Empty;
        public string SearchEmployee { get; set; } = string.Empty;
    }
}
