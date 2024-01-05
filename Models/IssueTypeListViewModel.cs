namespace Supermarket.Models
{
    public class IssueTypeListViewModel
    {
        public IEnumerable<IssueType> IssueType { get; set; }
        public PagingInfo Pagination { get; set; }
    }
}
