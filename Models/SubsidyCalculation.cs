namespace Supermarket.Models
{
    public class SubsidyCalculation
    {
        public int SubsidyCalculationId { get; set; }
        
        public int PontoId { get; set; }
        public Ponto? Ponto { get; set; }
        public int SubsidySetupId { get; set; }
        public SubsidySetup? SubsidySetup { get; set; }

       

    }

    

}
