using System.ComponentModel.DataAnnotations;
using System;

namespace Supermarket.Models
{
    public class Hallway
    {
        public int HallwayId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string description { get; set; }

        public int StoreId { get; set; }
        public Store? Store { get; set; }

        public ICollection<Shelf>? Shelf { get; set; }
    }
}
