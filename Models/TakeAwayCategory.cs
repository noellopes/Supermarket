using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class TakeAwayCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage = "Name must have maximum 50 character")]
        public string Name { get; set; }
    }
}