using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Reserva 
    {
        //PRIMARY KEY
        [Required]
        int ReservaId { get; set; }

        public string HistoricoReserva { get; set; }


        // FK EMPLOYEE
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        // FK DEPARTMENTS
        public int DepartmentsId { get; set; }

        public Departments? Departments { get; set; }


        /* FK SENHAS
          public int SenhasID { get; set; }

        public Senhas? Senhas { get; set; } */
    }
}
