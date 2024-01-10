using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Ponto
    {
        [Key]
        [Required]
        public int PontoId {  get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime? Date { get; set; }


        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string CheckInTime { get; set; }


        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string CheckOutTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string LunchStartTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string LunchEndTime { get; set; }


        //pode ser o tempo das horas extras ou vice versa
        //por exemplo: o josefino trabalha ate as 20:00, mas ele coitadinho ficou la mais um pouco para arrumar as caixas, e ficou la mais 1h,
        //com isso, o day Balance fica a +60min, que vai ser contabilidade (por mim) nas horas extras :)

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm.")]
        public required string RealCheckOutTime { get; set; }





    //vai ser uma picklist, onde vai ser selecionado depois quando o josefino entrar no supermercado:
    //por exemplo, o josefino entra as 9h, e por sorte, ele entrou antes da hora, por isso, na picklist vai ser selecionado 
    //a opção de "Ponto registrado"
    //mas, se o josefino so entrar por volta das 9h30, vai aparecer a opção de "ponto irregular" sendo assim
    //o josefino tem de fazer um documentário para justificar o seu atraso de 30min.
        [Required]
        public string Status { get; set; } 

        public string? Justificative { get; set; }



        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan ExtraHours { get; set; }

    }
}
