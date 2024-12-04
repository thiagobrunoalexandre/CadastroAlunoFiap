public class RelacionamentoAlunoTurma
{
    public int Id { get; set; }
    public int AlunoId { get; set; } // Chave estrangeira para Aluno
    public int TurmaId { get; set; } // Chave estrangeira para Turma
    public bool Status { get; set; } = true; // true para Ativo, false para Inativo
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
