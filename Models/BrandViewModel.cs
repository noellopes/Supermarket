namespace Supermarket.Models
{
    public class BrandViewModel
    {
        public List<Brand> Brands { get; set; }
        public PagingInfoProduct PagingInfoProduct { get; set; }
        public string SearchName { get; set; } = string.Empty;
    }
}
