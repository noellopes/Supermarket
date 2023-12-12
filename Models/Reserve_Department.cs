using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class  Reserve_Department
    {
        // FK EMPLOYEE
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public Employee? Employee { get; set; }

        // FK DEPARTMENTS

        [Required]
        public int DepartmentsId { get; set; }

        [Required]
        public Departments? Departments { get; set; }


        /* FK Ticket
          public int TicketID { get; set; }

        public Ticket? Ticket { get; set; } */
    }
}
