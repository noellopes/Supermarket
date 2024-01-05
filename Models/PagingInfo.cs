namespace Supermarket.Models
{
    public class PagingInfo
    {
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int NUMBER_PAGES_SHOW_BEFORE_AFTER = 5;
        //public int TotalItems { get; set; }
        //public int PageSize { get; set; }
        //public int CurrentPage { get; set; }
        //public const int NUMBER_PAGES_SHOW_BEFORE_AFTER = 5;

        public int TotalItems { get; set; }

        public int PageSize { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}