namespace Supermarket.Models
{
    public class SubsidyCalculationViewModel
    {
        public List<Ponto> Pontos { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public DateTime? SearchMonth { get; set; }

    }
}
