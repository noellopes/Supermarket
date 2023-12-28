namespace Supermarket.Models
{
    public class ProductDiscountsViewModel
    {
        public List<ProductDiscount> ProductDiscounts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchProduct { get; set; } = string.Empty;
        public string SearchValue { get; set; } = string.Empty;
        public DateTime? SearchStartDate { get; set; } = null;
        public DateTime? SearchEndDate { get; set; } = null;
        public float? SearchMinValue { get; set; } = null;
        public float? SearchMaxValue { get; set; } = null;
    }
}
