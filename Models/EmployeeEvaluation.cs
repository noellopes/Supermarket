namespace Supermarket.Models
{
    public class EmployeeEvaluation
    {
        public int EmployeeEvaluationId { get; set; }
        
        public string? Description { get; set; }
        [System.ComponentModel.DisplayName("Grade Number")]
        public int GradeNumber { get; set; }

        [System.ComponentModel.DisplayName("Employee Id")]
        public int? EmployeeId { get; set;}
    }
}
