using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models {
    public class Group {
        public int GroupId {  get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        public required string Objectives { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
