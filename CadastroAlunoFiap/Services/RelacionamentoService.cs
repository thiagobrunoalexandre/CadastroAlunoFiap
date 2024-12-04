using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RelacionamentoService
{
    private readonly IRelacionamentoRepository _repository;

    public RelacionamentoService(IRelacionamentoRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Cria ou atualiza um relacionamento entre aluno e turma.
    /// </summary>
    /// <param name="relacionamento">Objeto RelacionamentoAlunoTurma.</param>
    public async Task CreateOrUpdateRelacionamentoAsync(RelacionamentoAlunoTurma relacionamento)
    {
        // Verifica se o relacionamento já existe
        if (await _repository.ExistsAsync(relacionamento.AlunoId, relacionamento.TurmaId))
        {
            throw new Exception("Este aluno já está relacionado a esta turma.");
        }

        // Adiciona ou atualiza o relacionamento
        await _repository.AddOrUpdateAsync(relacionamento);
    }

    ///// <summary>
    ///// Obtém todos os alunos relacionados a uma turma.
    ///// </summary>
    ///// <param name="turmaId">ID da Turma.</param>
    ///// <returns>Lista de objetos Aluno.</returns>
    //public async Task<IEnumerable<Aluno>> GetAlunosByTurmaIdAsync(int turmaId)
    //{
    //    return await _repository.GetAlunosByTurmaIdAsync(turmaId);
    //}

    /// <summary>
    /// Obtém todos os relacionamentos de um aluno.
    /// </summary>
    /// <param name="alunoId">ID do Aluno.</param>
    /// <returns>Lista de relacionamentos do aluno.</returns>
    public async Task<IEnumerable<RelacionamentoAlunoTurma>> GetRelacionamentosPorAlunoAsync(int alunoId)
    {
        return await _repository.GetAllByAlunoIdAsync(alunoId);
    }

    /// <summary>
    /// Obtém todos os relacionamentos de uma turma.
    /// </summary>
    /// <param name="turmaId">ID da Turma.</param>
    /// <returns>Lista de relacionamentos da turma.</returns>
    public async Task<IEnumerable<RelacionamentoAlunoTurma>> GetRelacionamentosPorTurmaAsync(int turmaId)
    {
        return await _repository.GetAllByTurmaIdAsync(turmaId);
    }

    /// <summary>
    /// Inativa um relacionamento com base no ID.
    /// </summary>
    /// <param name="id">ID do relacionamento a ser inativado.</param>
    public async Task InactivateRelacionamentoAsync(int id)
    {
        // Inativa o relacionamento e verifica se a operação foi bem-sucedida
        var result = await _repository.InactivateAsync(id);
        if (result <= 0)
        {
            throw new Exception("Erro ao inativar o relacionamento.");
        }
    }

    /// <summary>
    /// Verifica se um relacionamento entre aluno e turma já existe.
    /// </summary>
    /// <param name="alunoId">ID do Aluno.</param>
    /// <param name="turmaId">ID da Turma.</param>
    /// <returns>True se o relacionamento existir, False caso contrário.</returns>
    public async Task<bool> ExistsRelacionamentoAsync(int alunoId, int turmaId)
    {
        return await _repository.ExistsAsync(alunoId, turmaId);
    }


    public async Task<List<Aluno>> GetAlunosByTurmaIdAsync(int turmaId)
    {
        try
        {
            var alunos = await _repository.GetAlunosByTurmaIdAsync(turmaId);
            return alunos.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter alunos relacionados à turma {turmaId}: {ex.Message}", ex);
        }
    }

}
