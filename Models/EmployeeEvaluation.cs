using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class EmployeeEvaluation
    {
        public int EmployeeEvaluationId { get; set; }

        public string? Description { get; set; }

        [System.ComponentModel.DisplayName("Grade Number")]
        [Required]
        [Range(1, 10)]
        public int GradeNumber { get; set; }

        [System.ComponentModel.DisplayName("Employee Id")]
        [Required]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
