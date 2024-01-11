using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Supermarket.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public required string Employee_Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Employee_Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Employee_Password { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be  9 digits long. ")]
        public required string Employee_Phone { get; set; }

        [Required]
        public required string Employee_NIF { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Employee_Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime? Employee_Birth_Date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime? Employee_Admission_Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Employee_Termination_Date { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string Standard_Check_In_Time { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]        
        public required string Standard_Check_Out_Time { get; set; }


        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string Standard_Lunch_Hour { get; set; }

        [Required]
        public required string Standard_Lunch_Time { get; set; }



        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Employee_Time_Bank { get; set; }

        public MealCard? MealCard { get; set; }

        public int IDDepartments { get; set; }
        public Department? Departments { get; set; }

       
        public ICollection<Purchase>? Purchases { get; set; }
    }
}