using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class EmployeeSchedule
    {
        [Key]
        [Required]
        public int EmployeeScheduleId {  get; set; }
        [Required]
        public required int EmployeeId { get; set; }
        [Required]
        public required DateTime Date { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string CheckInTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string CheckOutTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string LunchStartTime { get; set; }

        [Required]
        public required int LunchTime { get; set; }

        public Employee? Employee { get; set; }

    }
}
