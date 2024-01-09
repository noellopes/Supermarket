namespace Supermarket.Models
{
    public class ReserveDepartmentViewModel 
    {
       
            public IEnumerable<ReserveDepartment> ReserveDepartment { get; set; }
            public PagingInfo PagingInfo { get; set; }
            public string SearchEmployeeName { get; set; }
            public int SearchNumeroFunc { get; set; }
            public int SearchReserveId { get; set; }

        
    }
}
