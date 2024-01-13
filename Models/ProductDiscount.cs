<<<<<<< HEAD
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> FolgasPendentesAprovadas

namespace Supermarket.Models
{
    public class ProductDiscount
    {
        public int ProductDiscountId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [DisplayName("Client Card")]
        public int ClientCardId { get; set; }
        public ClientCard? ClientCard { get; set; }
        public float Value { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
