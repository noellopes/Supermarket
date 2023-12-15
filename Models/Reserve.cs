using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Reserve
    {
        //PRIMARY KEY
        [Required]

        [Key]
        public int ReserveId { get; set; }

        public ICollection<ReserveDepartment> Name { get; set; }
    }
}
