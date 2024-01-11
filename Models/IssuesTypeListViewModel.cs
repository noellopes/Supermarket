namespace Supermarket.Models
{
    public class IssuesTypeListViewModel
    {
        public IEnumerable<IssueType> IssueType { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchIssueType { get; set; } = string.Empty;
    }

}
