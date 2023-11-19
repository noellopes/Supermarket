using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Folga
    {
        [Key]
        public int FolgaId { get; set; }


        public string? Gestor { get; set; } = "";
        public string? Status { get; set; } = "";



        
        [DataType(DataType.Date)]
        public DateTime? DataPedido { get; set; }



        [Required]

        [DataType(DataType.Date)]
        public DateTime? DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DataFim { get; set; }
        [Required]
        public string? Motivo { get; set; } = "";
    }
}
