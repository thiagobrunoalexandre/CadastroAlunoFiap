public class Turma
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; 
    public string? Descricao { get; set; }
    public bool Status { get; set; } = true; 
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
