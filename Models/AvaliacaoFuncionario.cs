namespace Supermarket.Models
{
    public class AvaliacaoFuncionario
    {
        public int AvaliacaoFuncId { get; set; }
        
        public string? Description { get; set; }
        public int GradeNumber { get; set; }

        public int FuncionarioId { get; set;}
    }
}
