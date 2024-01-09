using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public enum Severity
    {
        Light,
        Moderated,
        Severe
    }
    public class Issues
    {
        [Key]
        public int IssueId { get; set; } // Issue Identifier

        //Relation with foreign key for Product
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        //Relation with foreign key for Issue Category
        [Display(Name = "Issue category")]
        public int IssueTypeId { get; set; }
        public IssueType? IssueType { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Details")]
        public string Description { get; set; } = string.Empty; // Issue Description

        //Relation with foreign key for Supplier
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        //Relation with foreign key for Employee
        [Display(Name = "Employee responsible for registration")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration date")]
        public DateTime IssueRegisterDate { get; set; } = DateTime.Now;

        [Required]
        [EnumDataType(typeof(Severity))]
        [Display(Name = "Severity")]
        public Severity Severity { get; set; }
    }
}
