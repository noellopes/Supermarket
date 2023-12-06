namespace Supermarket.Models
{
    public class Tickets
    {
       
        public required int TicketId { get; set; }

        public DateTime PrintDate { get; set; }


        public int TicketNumber { get; set;}

        public string TicketState { get; set; }

        public int DepartmentId { get; set; } 

        public Departments Departments { get; set; }

        public int ClientCard { get; set; }

        //public ClientCard ClientCard { get; set; }
    }
}
