namespace Supermarket.Models
{
    public class SchedulesViewModel
    {
        public List<Schedule> Schedules { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public PagingInfo PagingInfo { get; set; }

        public int SearchDepartment { get; set; } = 0;

        //public int SearchButtonDepartment { get; set; } = 0;
    }
}
