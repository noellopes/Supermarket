using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        [Required(ErrorMessage = "The Start Date is mandatory.")]
        [DisplayName("Starting date")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DisplayName("End date")]
        public DateTime? EndDate { get; set; } = null;
        [Required(ErrorMessage = "The Daily Start Date is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [DisplayName("Daily starting hour")]
        public DateTime DailyStartTime { get; set; } = new DateTime(2099, 04, 30, 09, 00, 0);
        [Required(ErrorMessage = "The Daily Finish Date is mandatory.")]
        public DateTime DailyFinishTime { get; set; }
        public int DeptID { get; set; }
        public Department Department { get; set; }
    }
}
