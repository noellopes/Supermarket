﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Reserve 
    {
        //PRIMARY KEY
        [Required]
        int ReservaId { get; set; }

        public required string HistoricoReserva { get; set; }

    }
}