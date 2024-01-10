namespace Supermarket.Models
{
    public class CategoryDiscountsViewModel
    {
        public List<CategoryDiscounts> CategoryDiscounts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchCategory { get; set; } = string.Empty;
        public float? SearchValue { get; set; }
        public DateTime? SearchStartDate { get; set; } = null;
        public DateTime? SearchEndDate { get; set; } = null;
    }
}
