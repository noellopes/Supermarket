using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Folga
    {

        public int FolgaId { get; set; }

<<<<<<< HEAD


=======
>>>>>>> 736de2404e9ecf82fd39e19fe017d233d07df1d4
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public int? GestorId { get; set; }
<<<<<<< HEAD
        public enum FolgaStatus
=======

        public enum  FolgaStatus
>>>>>>> 736de2404e9ecf82fd39e19fe017d233d07df1d4
        {
            Aprovada,
            Rejeitada,
            Pendente
        }

        public FolgaStatus? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataPedido { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataResultado { get; set; }

        [Required]
        [DataType(DataType.Date)]
<<<<<<< HEAD

=======
>>>>>>> 736de2404e9ecf82fd39e19fe017d233d07df1d4
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required]
        public Motivo? motivo { get; set; }
<<<<<<< HEAD








=======
>>>>>>> 736de2404e9ecf82fd39e19fe017d233d07df1d4
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
