using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class User_Order
    {
        public int Id { get; set; }


        [Required]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public TakeAwayProduct? Product { get; set; }




    }
}
