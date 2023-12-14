using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Formation
    {
        // PRIMARY KEY
        [Required]
        [Key]
        public int FormationId { get; set; }

        public string PontuacaoFormation { get; set; }

        // FK EMPLOYEE
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}

