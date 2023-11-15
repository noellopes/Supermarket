using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Folga
    {
        [Key]
        public int folgaId { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime dataPedido { get; set; }
        [DataType(DataType.Date)]
        public DateTime dataInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime dataFim { get; set; }
        public string motivo { get; set; }
        
    }
}
