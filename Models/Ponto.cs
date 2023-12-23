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

        public int DayBalance { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente";

        public string Justificative { get; set; } = "";

        public string CheckInCoordenates { get; set; } = "";

        public string CheckOutCoordenates { get; set; } = "";

    }
}
