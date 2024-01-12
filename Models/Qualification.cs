using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Supermarket.Models
{
    public class Qualification
    {
        [Key]
        [Required]
        public int QualificationId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public required Employee? Employee { get; set; }

        [ForeignKey("IDDepartments")]
        public int DepartmentId { get; set; }
        public required Department? Departments { get; set; }

        [Required]
        [Range(1, 3)]
        public int Level { get; set; }
    }
}