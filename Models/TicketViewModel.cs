namespace Supermarket.Models
{
    public class TicketViewModel
    {
        public List<Ticket> Tickets { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public PagingInfo PagingInfo { get; set; }

        public int SearchDepartment { get; set; } = 0;

    }
}