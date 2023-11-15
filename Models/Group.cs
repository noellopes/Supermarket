using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models {
    public class Group {
        public int GroupId {  get; set; }

        public int Number { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Objectives { get; set; }
    }
}
