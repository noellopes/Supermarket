namespace Supermarket.Models
{
    public class DepListViewModel
    {
        public IEnumerable<Departments> Departments { get; set; }
        public PagDep Pagination { get; set; }
}
}
