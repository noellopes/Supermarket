using Supermarket.Models;
using System.Collections.Generic;

public class ProductsListViewModel
{
    public IEnumerable<Departments> Departments { get; set; }
    public PagingInfo Pagination { get; set; }
    public int SelectedPageSize { get; set; }
    public List<Tickets> Tickets { get; set; }
    public Dictionary<int, double> MediasPorDepartamento { get; set; }
}
