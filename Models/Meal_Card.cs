using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Meal_Card
    {

        public int Card_Id { get; set; }

        [Required]
        private int Balance { get; set; }


    }
}
