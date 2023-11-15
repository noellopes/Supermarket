using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models {
    public class Student {
        public int StudentId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public required string Numero { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public ICollection<StudentTask>? Task {  get; set; }
    }
}
