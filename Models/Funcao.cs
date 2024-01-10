using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(FuncaoId))]
    public class Funcao
    {
        public int FuncaoId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string NomeFuncao { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string? DescricaoFuncao { get; set;}




    }
}
