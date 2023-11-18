﻿using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Folga
    {
        [Key]
        public int FolgaId { get; set; }

        [Required]
        public string Gestor { get; set; } = "";
        public string Status { get; set; } = "";


        [Required]

        [DataType(DataType.Date)]
        public DateTime DataPedido { get; set; }



        [Required]

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        
        
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        public string Motivo { get; set; }="";
        

        public  Folga()
        {
            DataPedido = DateTime.Now;

        }
    }
}