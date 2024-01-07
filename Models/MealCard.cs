using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    public class MealCard
    {
        [Key]
        public int MealCardId { get; set; }

        [Required]
        public int Balance { get; set; } = 0;

        // Chave estrangeira para o funcionario
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        // Propriedade de navegação para o funcionario associado a este cartão
        public Employee? Employee { get; set; }

        public ICollection<CardMovement>? CardMovements { get; set; }


    }
}