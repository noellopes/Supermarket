using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        [StringLength(50),MinLength(3)]
        required public string ClientName { get; set; }
        [Required]
        [StringLength(50), MinLength(3)]
        required public string ClientAdress { get; set; }
        [Required]
        [StringLength(50), MinLength(3)]
        required public string ClientEmail { get; set; }
        [Required]
        required public DateTime ClientBirth {  get; set; } 


    }
}
