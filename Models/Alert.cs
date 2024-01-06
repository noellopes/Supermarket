using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Alert
    {
        public int AlertId { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Please enter a valid description, must be minimum 3 leters lengt and max 80")]
        public string Description { get; set; } = string.Empty;

    }
}
