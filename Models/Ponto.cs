using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Ponto
    {
        [Key]
        [Required]
        public int PontoId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        //[Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public string? CheckInTime { get; set; }
        
        //[Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public string? CheckOutTime { get; set; }

        //[Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public string? LunchStartTime { get; set; }

        //[Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public string? LunchEndTime { get; set; }

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
<<<<<<< HEAD
        public string? RealCheckOutTime { get; set; }
=======
        public required string RealCheckOutTime { get; set; }





        //vai ser uma picklist, onde vai ser selecionado depois quando o josefino entrar no supermercado:
        //por exemplo, o josefino entra as 9h, e por sorte, ele entrou antes da hora, por isso, na picklist vai ser selecionado 
        //a opção de "Ponto registrado"
        //mas, se o josefino so entrar por volta das 9h30, vai aparecer a opção de "ponto irregular" sendo assim
        //o josefino tem de fazer um documentário para justificar o seu atraso de 30min.
        [Required]
        public string Status { get; set; } = "Pendente";

        public string Justificative { get; set; } = "";

>>>>>>> FolgasPendentesAprovadas

        public string? Status { get; set; } = "Pendente";

        public string? Justificative { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan DayBalance { get; set; }
        public bool DayBalancePositive { get; set; } = true;

    }
}
