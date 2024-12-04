using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync(); // Busca todos os alunos
    Task<Aluno?> GetByIdAsync(int id); // Busca aluno por ID
    Task<Aluno?> GetByUsuarioAsync(string usuario); // Busca aluno por nome de usuário (único)
    Task<int> AddAsync(Aluno aluno); // Adiciona um novo aluno
    Task<int> UpdateAsync(Aluno aluno); // Atualiza um aluno
    Task<int> InactivateAsync(int id); // Inativa um aluno
}
