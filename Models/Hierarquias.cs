
namespace Supermarket.Models
{
    public class Hierarquias
    {
        public int Id { get; set; }
        public int SuperiorId { get; set; } // Id do superior (chave estrangeira)
        public Employee? Superiores { get; set; } // Funcionário superior

        public int SubordinadoId { get; set; } // Id do subordinado (chave estrangeira)
        public Employee? Subordinados { get; set; } // Funcionário subordinado
    }
}