namespace Supermarket.Models
{
    public class MealCardTopViewModel
    {

        public List<Employee> Employees { get; set; }
        public int EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public List<CardMovement> CardMovement { get; set; }
        public List<MealCard> MealCards { get; set; }
        public DateOnly Start_Filter { get; set; }
        public DateOnly End_Filter { get; set; }

        public int Total_Spent { get; set; }
    }
}
