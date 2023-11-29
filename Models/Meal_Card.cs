using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(EmployeeId), nameof(Card_Id))]
    public class Meal_Card
    {

        public int Card_Id { get; set; }

        [Required]
        public int Balance { get; set; }

        // Chave estrangeira para o funcionario
        public int EmployeeId { get; set; }

        // Propriedade de navegação para o funcionario associado a este cartão
        public Employee Employee { get; set; }

        public ICollection<Card_Movement>? Movements { get; set; }


    }
}
