using Microsoft.AspNetCore.Mvc;

namespace Supermarket.Models
{
    public class Senhas 
    {
        public int TicketId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime? Atendimento { get; set; }

    }
}
