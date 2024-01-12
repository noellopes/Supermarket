namespace Supermarket.Models
{
    public class GrupoProjetoListViewModel
    {
        public IEnumerable<GrupoProjeto> grupoProjetos { get; set; }
        public PagingInfo Pagination { get; set; }
    }
}
