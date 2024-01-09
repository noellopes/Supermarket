using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(ProjetoId))]
    public class GrupoProjeto
    {
        public int ProjetoId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required String NomeProjeto { get; set; }


        [StringLength(50)]
        public string? DescricaoProjeto { get; set;}

        [Required]
        public required string Objectives { get; set; }

        public ICollection<Employee>? Employees { get; set; }

        public ICollection<Funcao>? Funcoes { get; set; }
    }

}
