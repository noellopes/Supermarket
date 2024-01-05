using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Ticket { 

        // Chave primária da entidade. O atributo [Key] é usado para marcar a chave primária.
        [Key]
        public int TicketId { get; set; }
        // Data de impressão do ticket.
        [Required]
        public DateTime DataEmissao { get; set; } = DateTime.Now;
        // Data de impressão do ticket.
        public DateTime? DataAtendimento { get; set; } = null;
        // Número do ticket.
        [Required]
        public int NumeroDaSenha { get; set; }
        // Estado atual do ticket.
        public bool Estado { get; set; } = false;

        // Estado atual do ticket.
        public bool Prioritario { get; set; }
        //para calcular valores das datas 
        public double TotalMinutesDifference
        {
            get
            {
                if (DataEmissao != default && DataAtendimento != default)
                {
                    TimeSpan difference = DataAtendimento - DataEmissao;
                    return difference.TotalMinutes;
                }
                return 0; // se nada for feito retorna 
            }
        }
        // Chave estrangeira para o departamento. Relaciona-se a outra entidade chamada "Departments".
        public int IDDepartments { get; set; }
        public Department? Departments { get; set; }

        // Chave estrangeira para o cartão do cliente. "ClientCard".
        // public int ClientCard { get; set; }
        // public ClientCard? ClientCard { get; set; }
    }
}
