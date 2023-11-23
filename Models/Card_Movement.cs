using Microsoft.VisualBasic;

namespace Supermarket.Models
{
    public class Card_Movement
    {
        public int Movement_Id { get; set; }

        public DateTime Movement_Date { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

        public string Type {  get; set; } 

        public int Card_Id { get; set; }

        public Meal_Card Meal_Card { get; set; }

    }
}
