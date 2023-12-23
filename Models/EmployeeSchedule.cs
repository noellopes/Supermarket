using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class EmployeeSchedule
    {
        public int EmployeeScheduleId {  get; set; }

        
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public TimeSpan LunchStartTime { get; set; }
        public TimeSpan LunchTime { get; set; }

    }
}
