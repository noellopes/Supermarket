using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }

        [StringLength(50,MinimumLength = 3)]
        [Required]
        public required string Funcionario_Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Funcionario_Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Funcionario_Senha { get; set; }

        [MaxLength(9)]
        public int Funcionario_Telefone { get; set; }


        [MaxLength(9)]
        public int Funcionario_NIF { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Funcionario_Morada { get; set; }

        [Required]
        public required DateOnly Funcionario_Data_Nasc { get; set; }

        [Required]
        public required DateOnly Funcionario_Data_Admissao { get; set; }

        [Required]
        public required DateOnly Funcionario_Data_Recisao { get; set; }

        [Required]
        public required DateTime Hora_Entrada_Padrao { get; set; }

        [Required]
        public required DateTime Hora_Saida_Padrao { get; set; }

        [Required]
        public required DateTime Hora_Almoco_Padrao { get; set; }

        [Required]
        public required DateTime Tempo_Almoco_Padrao { get;set; }

        [Required]
        public required DateTime Funcionario_Banco_Horas { get; set; }


    }
}
