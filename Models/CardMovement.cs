using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Supermarket.Models
{

    public class CardMovement
    {
        public int CardMovementId { get; set; }

        public DateTime Movement_Date { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

        public string Type {  get; set; } 

        public int Card_Id { get; set; }

        public MealCard MealCard { get; set; }

    }
}
