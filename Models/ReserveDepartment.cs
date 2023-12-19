using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class  ReserveDepartment
    {
        [Required]
        [Key]
        public int ReserveDepartmentId { get; set; }


        // FK RESERVE       
        [Required]
        [ForeignKey("ReserveId")]
        public Reserve? Reserve { get; set; }

        
        // FK EMPLOYEE
        //Relação de muitos para muitos
        [Required]
        [ForeignKey("EmployeeId")]
        public required List<Employee> Employees { get; set; }

        //// FK DEPARTMENTS
        ////Relação de muitos para muitos
        //[Required]
        //[ForeignKey("DepartmentsId")]
        //public required List<Departments> Departments { get; set; }

        // NUMERO DE FUNCIONARIOS NA RESERVA
        [Required]
        public int NumeroDeFunc { get; set; }
             
        /* FK Ticket
          public int TicketID { get; set; }

        public Ticket? Ticket { get; set; } */
    }
}
