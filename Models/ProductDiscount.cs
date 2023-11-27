﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ProductDiscount
    {
        public int ProductDiscountId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public required float Value { get; set; }
        [Required]
        public required  DateOnly InicialTime { get; set; }
        [Required]
        public required DateOnly FinalTime { get; set; }
    }
}
