namespace Supermarket.Models
{
    public class ProductViewModel
    {
        public List<Product> Product { get; set; }
        public PagingInfoProduct PagingInfoProduct { get; set; }
        public string SearchName { get; set; } = string.Empty;

        public string FilterStatus { get; set; } = string.Empty;
    }
}
