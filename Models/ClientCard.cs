using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ClientCard
    {

        public int ClientCardId { get; set; }

        public int ClientId { get; set; }
        public Client? Client {get;  set;}

        public int ClientCardNumber { get; set; }

        public float Balance { get; set; } = 0;


        public Boolean Estado { get; set; }

    }
}
