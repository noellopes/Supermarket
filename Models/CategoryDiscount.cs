namespace Supermarket.Models
{
    public class CategoryDiscount
    {
        public int CategoryDiscountId { get; set; }
        public int Value { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
