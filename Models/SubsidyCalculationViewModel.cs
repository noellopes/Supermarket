namespace Supermarket.Models
{
    public class SubsidyCalculationViewModel
    {
        public List<Employee> Employees { get; set; }
        public Ponto Pontoes { get; set; }
        public List<Ponto> ListaPonto { get; set; }
        public List<SubsidySetup> SubsidySetupList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
