namespace Supermarket.Models
{
    public class IssuesListViewModel
    {
        public IEnumerable<Issues> Issues { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; } = string.Empty;
        public string SearchIssueType { get; set; } = string.Empty;

        public string SearchSupplier { get; set; } = string.Empty;
        public string SearchEmployee { get; set; } = string.Empty;


    }
}
