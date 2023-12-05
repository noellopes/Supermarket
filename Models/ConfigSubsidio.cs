using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ConfigSubsidio
    {
  
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Formato de hora inválido. Use HH:mm")]
        public DateTime HorasMinTrabalhadas { get; set; }
        [Required]
        public float valorSubsidioDiario { get; set; }
        [Required]
        public  DateTime DataPagamentoMensal { get; set; }
       
    }
}
