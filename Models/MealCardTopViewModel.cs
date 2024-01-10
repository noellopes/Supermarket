namespace Supermarket.Models
{
    public class MealCardTopViewModel
    {

        public List<Employee> Employees { get; set; }
        public int EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public List<CardMovement> CardMovement { get; set; }
        public List<MealCard> MealCards { get; set; }
        public DateTime Start_Filter { get; set; }
        public DateTime End_Filter { get; set; }

        public List<TopEmployeeSpending> TopEmployees { get; set; }

        public List<Department> Departments { get; set; }
        public int SelectedDepartmentId { get; set; }
    }

    public class TopEmployeeSpending
    {
        public Employee Employee { get; set; }
        public decimal TotalSpent { get; set; }
    }
}

