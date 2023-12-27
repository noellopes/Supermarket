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
        public DateTime Date { get; set; }

        public TimeSpan CheckInTime { get; set; }

        public TimeSpan CheckOutTime { get; set; }

        public TimeSpan LunchStartTime { get; set; }

        public TimeSpan LunchEndTime { get; set; }


        //pode ser o tempo das horas extras ou vice versa
        //por exemplo: o josefino trabalha ate as 20:00, mas ele coitadinho ficou la mais um pouco para arrumar as caixas, e ficou la mais 1h,
        //com isso, o day Balance fica a +60min, que vai ser contabilidade (por mim) nas horas extras :)

        public int DayBalance { get; set; }



    //vai ser uma picklist, onde vai ser selecionado depois quando o josefino entrar no supermercado:
    //por exemplo, o josefino entra as 9h, e por sorte, ele entrou antes da hora, por isso, na picklist vai ser selecionado 
    //a opção de "Ponto registrado"
    //mas, se o josefino so entrar por volta das 9h30, vai aparecer a opção de "ponto irregular" sendo assim
    //o josefino tem de fazer um documentário para justificar o seu atraso de 30min.
        [Required]
        public string Status { get; set; } = "Pendente";

        public string Justificative { get; set; } = "";

     

    }
}
