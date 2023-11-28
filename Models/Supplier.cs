using System.ComponentModel.DataAnnotations;
namespace Supermarket.Models
{
    public class Supplier
    {

        public int SupplierId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public int Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string HeadQuarters { get; set; }

        [Required]
        [Range(0, 999999999)]
        public int Telefone { get; set; }
        public string Email { get; set; }



    }
}