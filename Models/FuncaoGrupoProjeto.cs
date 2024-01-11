using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models
{
    [PrimaryKey(nameof(FuncaoId),nameof(ProjetoId))]
    public class FuncaoGrupoProjeto
    {

        public int FuncaoId { get; set; }
        public Funcao funcao { get; set; }
        public int ProjetoId {  get; set; }
        public GrupoProjeto GrupoProjeto {  get; set; }
    }
}
