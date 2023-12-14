using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        [Required (ErrorMessage = "Please enter a Store Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter your Store Name bigger than 3 leters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Store Adress")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter your Store Adress bigger than 3 leters")]
        public required string Adress { get; set; }
    }
}
