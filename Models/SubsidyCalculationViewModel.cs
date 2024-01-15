namespace Supermarket.Models
{
    public class SubsidyCalculationViewModel
    {

        public List<Ponto> Pontos { get; set; }
        public List<SubsidyCalculation> SubsidyCalculations { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public string? SearchName { get; set; } = string.Empty;

    }
}
