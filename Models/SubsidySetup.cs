using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class SubsidySetup
    {
        public int SubsidySetupId { get; set; }
        [Required]

        //[RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Formato de hora inválido. Use HH:mm")]

        public DateTime HorasMinTrabalhadas { get; set; }
        [Required]
        public float ValorSubsidioDiario { get; set; }
        [Required]
        public DateTime DataPagamentoMensal { get; set; }

    }
}
