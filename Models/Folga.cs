using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class Folga
    {
        
        public int FolgaId { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public int? GestorId { get; set; }

        public enum  FolgaStatus
        {
            Aprovada,
            Rejeitada,
            Pendente
        }

        public FolgaStatus? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataPedido { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataResultado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required]
        public Motivo? motivo { get; set; }
        public Folga()
        {
            DataPedido = DateTime.Now;
        }
        public enum Motivo
        {
            [Display(Name = "Doença")] Doenca,
            [Display(Name = "Férias")] Ferias,
            [Display(Name = "Falecimento de Familiar")] FalecimentoFamiliar,
            [Display(Name = "Assuntos pessoais")] AssuntosPessoais,
            [Display(Name = "Assuntos Profissionais")] AssuntosProfissionais,
            [Display(Name = "Casamento de Familiar")] CasamentoFamiliar
        }
    }
}
