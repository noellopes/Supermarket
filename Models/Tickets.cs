using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Tickets
    {
        // Chave primária da entidade. O atributo [Key] é usado para marcar a chave primária.
        [Key]
        public int TicketId { get; set; }

        // Data de impressão do ticket.
        public DateTime DataEmicao { get; set; }
        // Data de impressão do ticket.
        public DateTime DataAtendimento { get; set; }
        // Número do ticket.
        public int NumeroDaSenha { get; set; }

        // Estado atual do ticket.
        public bool Estado { get; set; }

        // Estado atual do ticket.
        public bool Priorioritario { get; set; }

        // Chave estrangeira para o departamento. Relaciona-se a outra entidade chamada "Departments".
        public int DepartmentId { get; set; }
        public Departments? Departments { get; set; }

        // Chave estrangeira para o cartão do cliente. "ClientCard".
        // public int ClientCard { get; set; }
        // public ClientCard? ClientCard { get; set; }
    }
}
