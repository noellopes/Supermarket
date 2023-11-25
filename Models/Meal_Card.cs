﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Meal_Card
    {

        public int Card_Id { get; set; }

        [Required]
        public int Balance { get; set; }

        // Chave estrangeira para o funcionario
        public int EmployeeId { get; set; }

        // Propriedade de navegação para o funcionario associado a este cartão
        public Employee Employee { get; set; }

        public ICollection<Card_Movement> Movements { get; set; }


    }
}