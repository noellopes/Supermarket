using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(50,MinimumLength = 3)]
        [Required]
        public required string Employee_Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Employee_Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Employee_Password { get; set; }

        [MaxLength(9)]
        public int Employee_Phone { get; set; }


        [MaxLength(9)]
        public int Employee_NIF { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Employee_Address { get; set; }

        [Required]
        public required DateTime Employee_Birth_Date { get; set; }

        [Required]
        public required DateTime Employee_Admission_Date{ get; set; }

        [Required]
        public required DateTime Employee_Termination_Date { get; set; }

        [Required]
        public required DateTime Standard_Check_In_Time { get; set; }

        [Required]
        public required DateTime Standard_Check_Out_Time { get; set; }

        [Required]
        public required DateTime Hora_Almoco_Padrao { get; set; }

        [Required]
        public required DateTime Standard_Lunch_Time { get;set; }

        [Required]
        public required DateTime Employee_Time_Bank { get; set; }

        //Ligação de 1 para 1 com Meal_Card
        [Required]
        public Meal_Card Meal_Card { get; set; }


        //Ligação de 1 para 1 com Horario de atendimento (Schedule)
        //[Required]
        //public Schedule Schedule { get; set; }

        //Buscar a coleção das folgas (1 para muitos)
        //public ICollection<Day_Off> Days_Off { get; set;}

        //Buscar a coleção das Escalas (1 para muitos)
        //public ICollection<Scales>? Scales { get; set;}

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
