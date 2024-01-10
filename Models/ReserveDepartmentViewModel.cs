namespace Supermarket.Models
{
    public class ReserveDepartmentViewModel 
    {
       
            public required IEnumerable<ReserveDepartment1> ReserveDepartment { get; set; }
            public required PagingInfo PagingInfo { get; set; }
            public string SearchEmployeeName { get; set; } = string.Empty;
            

        
    }
}
