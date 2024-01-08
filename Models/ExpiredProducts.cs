using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    // Classe que representa produtos com data de validade expirada
    public class ExpiredProducts
    {
        // Identificador único para produtos expirados
        [Key]
        public int ExpiredProductId { get; set; }


        // Identificador do produto associado (relacionamento com a classe Product)
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        //// Data de fabricação do produto expirado
        //[DataType(DataType.DateTime)]
        //[Display(Name = "Fabrication Date")]
        //public DateTime FabricationDate { get; set; }


        // Data de validade do produto expirado
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }


        // Código de barras do produto expirado
        [Required]
        [Display(Name = "Batch Number")]
        public string BatchNumber { get; set; } = string.Empty;


        // Identificador do fornecedor associado (relacionamento com a classe Supplier)
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }


        // Identificador do funcionário responsável pelo registro do produto expirado (relacionamento com a classe Employee)
        [Display(Name = "Employee responsible for registration")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Display(Name = "Purchase")]
        public int? PurchaseId { get; set; }
        public Purchase? Purchase { get; set; }
    }
}
