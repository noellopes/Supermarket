namespace Supermarket.Models
{
    public class SchedulesViewModel
    {
        public List<Schedule> Schedules { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string SearchDepartment { get; set; } = string.Empty;
    }
}
