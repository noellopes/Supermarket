namespace Supermarket.Models
{
    public class EmployeesViewModel
    {
        public List<Employee> Employees { get; set;}

        public PagingInfo PagingInfo { get; set;}

        public string SearchName { get; set;} = string.Empty;
    }
}
