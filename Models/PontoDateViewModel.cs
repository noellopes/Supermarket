namespace Supermarket.Models
{
    public class PontoDateViewModel
    {
        public List<Ponto> Pontos { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public DateTime? SearchMonth { get; set; }
    }
}
