using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models
{
    public class Department
    {
        // Chave primária
        [Key]
        public int IDDepartments { get; set; }
        // Nome do Departamento
        [Required(ErrorMessage = "The Department Name field is mandatory.")]
        [StringLength(100, ErrorMessage = "The Department Name field must have a maximum of 100 characters.")]
        public required string NameDepartments { get; set; }
        // Descrição do Departamento
        [StringLength(255, ErrorMessage = "The Department Description field must have a maximum of 255 characters.")]
        public required string DescriptionDepartments { get; set; }
        // Descrição do Estado do departamento
        public required bool StateDepartments { get; set; }
        // Qualificaçao funcionario no Departamento
        [StringLength(5, ErrorMessage = "The Department Description select field select.")]
        public required string SkillsDepartments { get; set; }
        //Quantidade de senhas que cada gestor de departamento definir para o calculo da media
        [Required(ErrorMessage = "O campo QuatDepMed é obrigatório.")]
        [Range(1, 100, ErrorMessage = "O valor de QuatDepMed deve ser maior que 0 e me que 100.")]
        public required int QuatDepMed { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}

