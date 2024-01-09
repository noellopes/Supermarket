namespace Supermarket.Models
{
    public class SubsidyCalculationViewModel
    {
   
        public List<Ponto> Pontos { get; set; }
        public List<SubsidyCalculation> SubsidyCalculations { get; set; }
        public List<CardMovement> CardMovements { get; set; }

        public PagingInfo PagingInfo { get; set; }

        
        public string? SearchName { get; set; } = string.Empty;


        public List<PutbalanceOnCard> PutbalanceOnCards { get; set; }
      //  public List<Pontotest> Pontotests { get; set; }
    }

    public class PutbalanceOnCard
    {
        public CardMovement CardMovement { get; set; }
        public List<CardMovement> CardMovements { get; set; }
        public List<Ponto> Pontos { get; set; }
        public Ponto Ponto { get; set; }
        public List<SubsidySetup> SubsidySetups { get; set; }
        public SubsidySetup SubsidySetup { get; set; }

    }

  
}

