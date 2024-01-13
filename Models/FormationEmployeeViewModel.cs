using Microsoft.AspNetCore.Mvc;

namespace Supermarket.Models
{
    public class FormationEmployeeViewModel
    {
        public List<FormationEmployee> FormationEmployees { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchEmployeeName { get; set; }
        public string SearchFormationName { get; set; }
    }
}
