namespace Supermarket.Models
{
    public class MealCardEmployeesViewModel
    {
        public List<Employee> Employees { get; set;}

        public List<CardMovement> CardMovements { get; set;}

        public PagingInfo MealCardPagingInfo { get; set;}

        public string SearchName { get; set;} = string.Empty;

        public bool SOEwithoutMC { get; set; } = false;

        public bool SOEwithMC { get; set; } = false;

        public PagingInfo CardMovementPagingInfo { get; set;}

        public float Balance { get; set;}

        public string EmployeeName { get; set;}

        public int MealCard {  get; set;}
    }
}
