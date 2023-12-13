using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ClientCard
    {
        [Key]
        public int ClientCardId { get; set; }

        
        [StringLength(6)]
        required public string ClientCardNumber { get; set; }

        public float  Balance { get; set; }
    }
}
