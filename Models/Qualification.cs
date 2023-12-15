using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Supermarket.Models
{
    public class Qualification
    {
        //PRIMARY KEY
        [Required]
        public int QualificationId { get; set; }

        //DEPARTMENTS NUMBER 
        public required string DepartmentsN { get; set; }

        //LEVEL QUALIFICATION
        [Required]
        [Range(1, 3)]
        public int Level { get; set; }
    }
}
