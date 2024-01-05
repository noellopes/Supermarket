namespace Supermarket.Models
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; set; }
        public PagingInfoProduct PagingInfoProduct { get; set; }
        public string SearchName { get; set; } = string.Empty;
    }
}
