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

        [Required]
        public int NumeroDeFunc { get; set; }
    }
}
