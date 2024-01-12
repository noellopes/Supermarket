using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Ponto
    {
        [Key]
        [Required]
        public int PontoId {  get; set; }

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
        public string? RealCheckOutTime { get; set; }

        public string? Status { get; set; } = "Pendente";

        public string? Justificative { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan DayBalance { get; set; }
        public bool DayBalancePositive { get; set; } = true;

    }
}
