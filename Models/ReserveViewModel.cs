namespace Supermarket.Models
{
    public class ReserveViewModel
    {
        public required IEnumerable<Reserve> Reserve{ get; set; }
        public required PagingInfo PagingInfo { get; set; }
        public int SearchNumeroFunc { get; set; }


    }
}
