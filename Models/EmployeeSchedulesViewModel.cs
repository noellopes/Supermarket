namespace Supermarket.Models
{
    public class EmployeeSchedulesViewModel
    {
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
