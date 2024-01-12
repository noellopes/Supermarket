namespace Supermarket.Models
{
    public class PontoDateViewModel
    {
        public List<Ponto> Pontos { get; set; } = new List<Ponto>();
        public Employee? Employee { get; set; }
        public Ponto? PontoDia { get; set; }
    }
}
