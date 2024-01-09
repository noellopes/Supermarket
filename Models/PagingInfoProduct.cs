namespace Supermarket.Models
{
    public class PagingInfoProduct
    {
        public const int NUMBER_PAGES_SHOW_BEFORE_AFTER = 5;

        public int TotalItems { get; set; }

        public int PageSize { get; set; } = 3;

        public int CurrentPage { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
