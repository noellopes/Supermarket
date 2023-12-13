﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class  Reserve_Department
    {
        // FK RESERVE
        [Required]
        public int ReserveId { get; set; }

        [Required]
        public Reserve? Reserve { get; set; }

        // FK EMPLOYEE
        [Required]
        public int EmployeeId { get; set; }

        //Relação de muitos para muitos
        [Required]
        public required List<Employee> Employees { get; set; }

        // FK DEPARTMENTS

        [Required]
        public int DepartmentsId { get; set; }

        //Relação de muitos para muitos
        [Required]
        public required List<Departments> Departments { get; set; }

        // NUMERO DE FUNCIONARIOS NA RESERVA
        [Required]
        public int NumeroDeFunc { get; set; }

        
        

        

        /* FK Ticket
          public int TicketID { get; set; }

        public Ticket? Ticket { get; set; } */
    }
}
