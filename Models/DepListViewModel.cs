using Supermarket.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DepListViewModel
{
    public IEnumerable<Departments> Departments { get; set; }
    public PagingInfo Pagination { get; set; }
    public int SelectedPageSize { get; set; }
    public List<TimeSpan> TimeDifferences { get; set; }


}
