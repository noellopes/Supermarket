using System.ComponentModel.DataAnnotations;
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
        //[Display(Name = "Product")]
        //public int ProductId { get; set; }
        //public Product? Product { get; set; }

        //Relation with foreign key for Issue Category
        [Display(Name = "Issue Category")]
        public int IssueTypeId { get; set; }
        public IssueType? IssueType { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; } = string.Empty; // Issue Description

        //Relation with foreign key for Supplier
        //[Display(Name = "Supplier")]
        //public int SupplierID { get; set; }
        //public Supplier? Supplier { get; set; }

        //Relation with foreign key for Client
        //[Display(Name = "Client")]
        //public int IssueTypeId { get; set; }
        //public IssueType? IssueType { get; set; }

        //Relation with foreign key for Employee
        //[Display(Name = "Employee")]
        //public int EmployeeId { get; set; }
        //public Employee? Employee { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Issue Registration Date")]
        public DateTime IssueRegisterDate { get; set; }  //Registration date for the issue

        [Required]
        [EnumDataType(typeof(Severity))]
        public Severity Severity { get; set; }
    }
}
