using System.ComponentModel.DataAnnotations;
namespace Supermarket.Models
{
    public class PurchaseSupplier
    {
        public int PurchaseSupplierId { get; set; }
        [Required]

        public int SubTotal { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

    }

}