using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(ReserveId), nameof(EmployeeId))]
    public class ReserveDepartment1
    {
        // FK RESERVE       
        public int ReserveId { get; set; }
        public Reserve? Reserve { get; set; }


        // FK EMPLOYEE
        //Relação de muitos para muitos
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        //FK DEPARTMENTS
        //Relação de muitos para muitos
        //public int ID_Departments { get; set; }
        //public Departments Departments { get; set; }

        // NUMERO DE FUNCIONARIOS NA RESERVA
        [Required]
        public int NumeroDeFunc { get; set; }
        

        /* FK Ticket
          public int TicketID { get; set; }

        public Ticket? Ticket { get; set; } */
    }
}
