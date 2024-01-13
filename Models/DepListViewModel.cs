using Supermarket.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DepListViewModel
{
    public IEnumerable<Department> Departments { get; set; }
    public PagingInfo Pagination { get; set; }
    public int SelectedPageSize { get; set; }
    public List<TimeSpan> TimeDifferences { get; set; }
    public List<DepAverageTimeViewModel> AverageTimes { get; set; } = new List<DepAverageTimeViewModel>();
    // Adicione a propriedade para armazenar a média de senhas
    public List<int> AverageNumberOfTickets { get; set; }

}
