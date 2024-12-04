public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty; 
    public bool Status { get; set; } = true;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public int RelacionamentoId { get; set; }

    public int TurmaId { get; set; } // ID
}
