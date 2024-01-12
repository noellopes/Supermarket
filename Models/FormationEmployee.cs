using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class FormationEmployee
    {
        [Key]
        public int FormationEmployeeId { get; set; }


        [ForeignKey("FormationId")]
        public int FormationId { get; set; }
        public required Formation? Formation { get; set; }


        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public required Employee? Employee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "O valor deve estar entre 0 e 100")]
        public int NotaAtribuida { get; set; }
    }
}