using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ReduceProduct
    {
        public int ReduceProductId { get; set; }

        [Required(ErrorMessage = "Please introduce a valid reason to Reduce this Product")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Please enter your Reason, must be minimum 3 leters length")]
        public required string Reason { get; set; }

        [Required]
        [RegularExpression("^(Pending|Confirmed|Refused)$", ErrorMessage = "Invalid status")]
        public required string Status { get; set; } = "Pending";

        public DateTime Date { get; set;} = DateTime.Now;

        [Range(0, 999, ErrorMessage = "The quantity must be greater than zero")]
        public int Quantity { get; set;}

        //Error when creating scaffolding of ReduceProduct 
        //public int EmployeeId { get; set; }

        //public Employee? Employee { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int? WarehouseSectionId { get; set; }

        public WarehouseSection? WarehouseSection { get; set; }

        public int? ShelfId { get; set; }

        public Shelf? Shelf { get; set; }


    }
}
