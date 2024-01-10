namespace Supermarket.Models
{
    public class AfluenciasViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public Department? Department { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public DateTime? SearchDataIntervaloInicial { get; set; } = null;
        public DateTime? SearchDataIntervaloFinal { get; set; } = null;

        public int SearchLimiteAfluencia { get; set; } = 0;

        //public int SearchButtonDepartment { get; set; } = 0;
    }
}
