using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Addresses
    {
        [Key]
        public int AddressesId { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }


    }
}