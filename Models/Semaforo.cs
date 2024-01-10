using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Semaforo 
    {
        [Key]
        public int SemaforoId { get; set; }


        [ForeignKey("IDDepartments")]
        public int IDDepartments { get; set; }
        public required Department? Department { get; set; }


        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public required Ticket? Ticket { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "O valor deve estar entre 0 e 100")]
        public int NSenhasNoDepartamento { get; set; }

    }
}
