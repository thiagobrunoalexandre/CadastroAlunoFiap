using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class RelacionamentoRepository : IRelacionamentoRepository
{
    private readonly string _connectionString;

    public RelacionamentoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Obter todos os relacionamentos de um aluno usando a procedure
    public async Task<IEnumerable<RelacionamentoAlunoTurma>> GetAllByAlunoIdAsync(int alunoId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<RelacionamentoAlunoTurma>(
            "GetRelacionamentosByAlunoId",
            new { AlunoId = alunoId },
            commandType: CommandType.StoredProcedure
        );
    }

    // Obter todos os relacionamentos de uma turma usando a procedure
    public async Task<IEnumerable<RelacionamentoAlunoTurma>> GetAllByTurmaIdAsync(int turmaId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<RelacionamentoAlunoTurma>(
            "GetRelacionamentosByTurmaId",
            new { TurmaId = turmaId },
            commandType: CommandType.StoredProcedure
        );
    }

    // Criar ou Atualizar Relacionamento usando a procedure
    public async Task AddOrUpdateAsync(RelacionamentoAlunoTurma relacionamento)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(
            "AddOrUpdateRelacionamento",
            new { relacionamento.AlunoId, relacionamento.TurmaId },
            commandType: CommandType.StoredProcedure
        );
    }

    // Inativar Relacionamento usando a procedure
    public async Task<int> InactivateAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            "InactivateRelacionamento",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    // Obter relacionamento entre Aluno e Turma usando a procedure
    public async Task<RelacionamentoAlunoTurma?> GetByAlunoAndTurmaAsync(int alunoId, int turmaId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<RelacionamentoAlunoTurma>(
            "GetRelacionamentoByAlunoAndTurma",
            new { AlunoId = alunoId, TurmaId = turmaId },
            commandType: CommandType.StoredProcedure
        );
    }

    // Verificar se um relacionamento entre Aluno e Turma já existe usando Stored Procedure
    public async Task<bool> ExistsAsync(int alunoId, int turmaId)
    {
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.ExecuteScalarAsync<int>(
            "CheckRelacionamentoExists", // Nova Stored Procedure sugerida
            new { AlunoId = alunoId, TurmaId = turmaId },
            commandType: CommandType.StoredProcedure
        );
        return result > 0;
    }

    // Obter todos os alunos de uma turma usando Stored Procedure
    public async Task<IEnumerable<Aluno>> GetAlunosByTurmaIdAsync(int turmaId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Aluno>(
            "GetAlunosByTurmaId", // Nova Stored Procedure sugerida
            new { TurmaId = turmaId },
            commandType: CommandType.StoredProcedure
        );
    }



}
