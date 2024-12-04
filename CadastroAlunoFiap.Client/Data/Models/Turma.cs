using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroAlunoFiap.Client.Data.Models
{
    public class Turma
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da turma deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
        public string? Descricao { get; set; }

      
    }
}
