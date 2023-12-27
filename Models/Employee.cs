using System.ComponentModel.DataAnnotations;

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

        
        public int Employee_Time_Bank { get; set; }


        //Ligação de 1 para 1 com Meal_Card
        public MealCard? MealCard { get; set; }

        //public EmployeeSchedule? EmployeeSchedule { get; set; }

     


        //Ligação de 1 para 1 com Horario de atendimento (Schedule)
        //[Required]
        //public Schedule Schedule { get; set; }

        //Buscar a coleção das folgas (1 para muitos)
        //public ICollection<Day_Off> Days_Off { get; set;}

        //Buscar a coleção das Escalas (1 para muitos)
        //+public ICollection<Scales>? Scales { get; set;}

        //Buscar a coleção dos Pontos (1 para muitos)
        //public ICollection<Point> Point { get; set;}

        //Buscar a coleção dos Alerta (1 para muitos)
        //public ICollection<Alerta> Alerta { get; set;}

        //Buscar a coleção dos Abate_Funcionario (1 para muitos)
        //public ICollection<Employee_Reduction> Employee_Reduction { get; set;}

        //Buscar a coleção das Qualificação (Muitos para muitos)
        //public ICollection<Qualifications> Qualifications { get; set;}

        //Buscar a coleção do Departamento (Muitos para muitos)
        //public ICollection<Departments> Departments { get; set;}







    }
}