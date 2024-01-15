using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Reserve
    {
        //PRIMARY KEY
        [Required]

        [Key]
        public int ReserveId { get; set; }

        [Required(ErrorMessage = "O campo NumeroDeFunc é obrigatório.")]
        [Range(1, 30, ErrorMessage = "O valor de NumeroDeFunc deve ser maior que zero e menor ou igual a 30.")]
        public int NumeroDeFunc { get; set; }
    }
}
