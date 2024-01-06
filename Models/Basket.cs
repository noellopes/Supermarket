﻿namespace Supermarket.Models
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<TakeAwayProduct>();
        }

        public List<TakeAwayProduct> Products { get; set; }
        public double TotalPrice { get; set; }
        public int EstimatedPreparationTimeAsMinutes { get; set; }
        public int? TotalProductQuantityInBasket { get; set; }
    }
}
