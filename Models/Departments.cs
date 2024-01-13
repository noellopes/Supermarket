using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Departments
    {
        // Chave primária
        public required int ID_Departments { get; set; }

        // Nome do Departamento
        [Required(ErrorMessage = "The Department Name field is mandatory.")]
        [StringLength(100, ErrorMessage = "The Department Name field must have a maximum of 100 characters.")]
        public required string Name_Departments { get; set; }

        // Descrição do Departamento
        [StringLength(255, ErrorMessage = "The Department Description field must have a maximum of 255 characters.")]
        public required string Description_Departments { get; set; }

        // Chave estrangeira para o ID_Horario
        public int ID_Schedule { get; set; }

        // Propriedade de navegação para o Horario relacionado
        public Schedule? Schedule { get; set; }

    }
}
