using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Departments
    {
        // Chave primária
        [Key]
        public required int IDDepartments { get; set; }
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
        [StringLength(155, ErrorMessage = "The Department Description field must have a maximum of 255 characters.")]
        public required string SkillsDepartments { get; set; }
        //Quantidade de senhas que cada gestor de departamento definir para o calculo da media
        public required int QuatDepMed { get; set; }
    }
}

