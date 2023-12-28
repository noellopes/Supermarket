namespace Supermarket.Models
{
    public class MealCardEmployeesViewModel
    {
        public List<Employee> Employees { get; set;}

        public PagingInfo PagingInfo { get; set;}

        public string SearchName { get; set;} = string.Empty;

        public bool SOEwithoutMC { get; set; } = false;

        public bool SOEwithMC { get; set; } = false;

    }
}
