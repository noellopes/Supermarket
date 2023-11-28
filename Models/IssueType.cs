using System.ComponentModel.DataAnnotations;
namespace Supermarket.Models
{
    public class IssueType
    {
        public int IssueTypeId { get; set; } // Issue Type Category Identifier

        [Required]
        [StringLength(50, MinimumLength = 7)]
        public required string Name { get; set; } // Issue Type Category Name

        [Required]
        [MinLength(10)]
        [Display(Name = "Issue Category Description")]
        public string IssueDescription { get; set; } = string.Empty; // Issue Type Category Description   

        public ICollection<IssueType>? IssueTypes { get; set; }
    }
}
