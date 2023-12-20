namespace Supermarket.Models
{
    public class PagDep
    {
        public const int DEFAULT_PAGE_SIZE = 5;
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
