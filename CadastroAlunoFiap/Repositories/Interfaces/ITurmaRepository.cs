using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetAllAsync(); // Busca todas as turmas
    Task<Turma?> GetByIdAsync(int id); // Busca turma por ID
    Task<Turma?> GetByNomeAsync(string nome); // Busca turma por nome (único)
    Task<int> AddAsync(Turma turma); // Adiciona uma nova turma
    Task<int> UpdateAsync(Turma turma); // Atualiza uma turma
    Task<int> InactivateAsync(int id); // Inativa uma turma
}
