namespace Supermarket.Models
{
    public class AfluenciasViewModel
    {
        public List<Tickets> Tickets { get; set; }

        public Departments? Departments { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public DateTime? SearchDataIntervaloInicial { get; set; } = null;
        public DateTime? SearchDataIntervaloFinal { get; set; } = null;

        //public int SearchButtonDepartment { get; set; } = 0;
    }
}
