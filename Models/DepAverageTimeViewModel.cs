namespace Supermarket.Models
{
    public class DepAverageTimeViewModel
    {
        public int DepartmentId { get; set; }
        public TimeSpan AverageTime { get; set; } //= TimeSpan.FromMinutes(1);
    }
}
