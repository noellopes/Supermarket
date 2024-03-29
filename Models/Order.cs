﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Status { get; set; } = "Preparing";

        [Required]
        public float TotalPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int Quantity { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int EstimatedPreparationTimeAsMinutes { get; set; }
        public List<TakeAwayProduct> Products { get; set; }

        public List<User_Order> UserOrders { get; set; }

    }
}
