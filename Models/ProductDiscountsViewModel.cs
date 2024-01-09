namespace Supermarket.Models
{
    public class ProductDiscountsViewModel
    {
        public List<ProductDiscount> ProductDiscounts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchProduct { get; set; } = string.Empty;
        public float? SearchValue { get; set; }
        public DateTime? SearchStartDate { get; set; } = null;
        public DateTime? SearchEndDate { get; set; } = null;
        public List<ClientCard> ClientsWithBirthday { get; set; }

    }
}

