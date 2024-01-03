using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class FormationEmployee
    {
        [Key]
        public int FormationEmployeeId { get; set; }

        [Required]
        [ForeignKey("FormationId")]
        public int FormationId { get; set; }

        public Formation Formation { get; set; }
        [Required]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public float NotaAtribuida { get; set; }
    }
}
