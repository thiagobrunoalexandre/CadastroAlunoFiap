using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TurmaRepository : ITurmaRepository
{
    private readonly string _connectionString;

    public TurmaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Turma>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Turma>("SELECT * FROM Turma WHERE Status = 1");
    }

    public async Task<Turma?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Turma>(
            "SELECT * FROM Turma WHERE Id = @Id AND Status = 1", new { Id = id });
    }

    public async Task<Turma?> GetByNomeAsync(string nome)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Turma>(
            "SELECT * FROM Turma WHERE Nome = @Nome AND Status = 1", new { Nome = nome });
    }

    public async Task<int> AddAsync(Turma turma)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "INSERT INTO Turma (Nome, Descricao, Status) VALUES (@Nome, @Descricao, @Status)";
        return await connection.ExecuteAsync(query, turma);
    }

    public async Task<int> UpdateAsync(Turma turma)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE Turma SET Nome = @Nome, Descricao = @Descricao WHERE Id = @Id";
        return await connection.ExecuteAsync(query, turma);
    }

    public async Task<int> InactivateAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE Turma SET Status = 0 WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
