using System.ComponentModel.DataAnnotations;

namespace CadastroAlunoFiap.Client.Data.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Usuário é obrigatório")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public int RelacionamentoId { get; set; } // ID
        public int TurmaId { get; set; } // ID
      
    }
}
