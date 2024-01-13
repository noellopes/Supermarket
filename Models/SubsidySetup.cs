using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class SubsidySetup
    {
        public int SubsidySetupId { get; set; }
        [Required]


        public float HorasMinTrabalhadas { get; set; }
        [Required]
        public float ValorSubsidioDiario { get; set; }
        [Required]
        public DateTime DataEntradaVigor { get; set; }

    }
}
