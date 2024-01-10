using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Orders
    {
        public int OrdersId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("ClientCard")]
        public int ClientCardId { get; set; }
        public ClientCard? ClientCard { get; set; }

    }
}
