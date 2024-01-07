namespace Supermarket.Models
{
    public class FuncaoListViewModel
    {
        public IEnumerable<Funcao> funcao { get; set; }
        public PagingInfo Pagination { get; set; }
    }
}
