namespace Supermarket.Models
{
    public class EmployeeEvaluationListViewModel
    {
        public IEnumerable<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public PagingInfo Pagination { get; set; }
    }
}
