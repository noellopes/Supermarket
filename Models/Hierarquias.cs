using System;
using System.ComponentModel.DataAnnotations;
namespace Supermarket.Models
{
    public class HierarquiasModel
    {
        [Key]
        public int HierarquiaId { get; set; }
        public string ?HierarquiaNome { get; set; }
        public int NivelHierarquico { get; set; }
    }

}

