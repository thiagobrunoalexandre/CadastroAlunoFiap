public class RelacionamentoAlunoTurma
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public bool Status { get; set; }
    public DateTime DataCriacao { get; set; }

    public string AlunoNome { get; set; } = string.Empty;
    public string TurmaNome { get; set; } = string.Empty;
}
