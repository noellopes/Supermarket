using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class FuncionarioDepartamento
    {
        public int FuncionarioId { get; set; }
        public int DepartamentoId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataSaida { get; set; }
        [Required]
        public string NivelAcesso { get; set; } = "";

        //Chave estrangeiras
        public Department Departamento { get; set; }

        public ICollection<Employee> Funcionario { get; set; } 

    }
}
