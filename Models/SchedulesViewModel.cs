namespace Supermarket.Models
{
    public class SchedulesViewModel
    {
        public List<Schedule> Schedules { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public PagingInfo PagingInfo { get; set; }

        public string SearchDepartment { get; set; } = string.Empty;

        //public int SearchButtonDepartment { get; set; } = 0;
    }
}
