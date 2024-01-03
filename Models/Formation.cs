using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Formation
    {
        [Key]
        public int FormationId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Formation_Name { get; set; }
    }
}
