using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        public required string Description { get; set; }

        [Range(0, 100)]
        public float PercentageCompleted { get; set; } = 0;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
        public ICollection<StudentTask>? Student { get; set; }
    }
}
