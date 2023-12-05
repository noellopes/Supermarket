using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ClientCard
    {
        public int ClientCard_Id { get; set; }

        
        [StringLength(6)]
        required public string ClientCard_Number { get; set; }

        public float  Balance { get; set; }
    }
}
