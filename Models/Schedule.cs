using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        [Required(ErrorMessage = "The Start Date is mandatory.")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = null;
        [Required(ErrorMessage = "The Daily Start Date is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime DailyStartTime { get; set; } = new DateTime(2099, 04, 30, 09, 00, 0);
        [Required(ErrorMessage = "The Daily Finish Date is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime DailyFinishTime { get; set; } = new DateTime(2099, 04, 30, 18, 00, 0);
        public int IDDepartments { get; set; }
        public Department? Departments { get; set; }
    }
}
