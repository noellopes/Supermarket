namespace Supermarket.Models
{
    public class EmployeeSchedulesViewModel
    {
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
