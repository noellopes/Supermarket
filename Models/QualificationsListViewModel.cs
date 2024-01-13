using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class QualificationListViewModel
    {
        public IEnumerable<Qualification> Qualifications { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchNameEmp { get; set; } = string.Empty;

        public string SearchNameDep { get; set; } = string.Empty;

        public int SearchLevel { get; set; }
    }
}
