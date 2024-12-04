using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelacionamentoRepository
{
    /// <summary>
    /// Verifica se um relacionamento entre Aluno e Turma já existe e está ativo.
    /// </summary>
    /// <param name="alunoId">ID do Aluno</param>
    /// <param name="turmaId">ID da Turma</param>
    /// <returns>Retorna verdadeiro se o relacionamento existir e estiver ativo.</returns>
    Task<bool> ExistsAsync(int alunoId, int turmaId);

    /// <summary>
    /// Cria ou atualiza um relacionamento entre Aluno e Turma.
    /// </summary>
    /// <param name="relacionamento">Objeto de relacionamento contendo os IDs e outros dados.</param>
    /// <returns>Task concluída quando a operação for finalizada.</returns>
    Task AddOrUpdateAsync(RelacionamentoAlunoTurma relacionamento);

    /// <summary>
    /// Obtém todos os alunos associados a uma turma.
    /// </summary>
    /// <param name="turmaId">ID da Turma</param>
    /// <returns>Lista de alunos associados à turma.</returns>
    Task<IEnumerable<Aluno>> GetAlunosByTurmaIdAsync(int turmaId);

    /// <summary>
    /// Inativa um relacionamento com base no ID.
    /// </summary>
    /// <param name="id">ID do relacionamento a ser inativado.</param>
    /// <returns>O número de linhas afetadas.</returns>
    Task<int> InactivateAsync(int id);
    Task<IEnumerable<RelacionamentoAlunoTurma>> GetAllByTurmaIdAsync(int turmaId);
    Task<IEnumerable<RelacionamentoAlunoTurma>> GetAllByAlunoIdAsync(int alunoId);
}
