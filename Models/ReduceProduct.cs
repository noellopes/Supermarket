using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class ReduceProduct
    {
        public int ReduceProductId { get; set; }

        [Required]
        public required string Reason { get; set; }

        [Required]
        public required string Status { get; set; } = "Pending";

        public DateTime Date { get; set;} = DateTime.Now;

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
