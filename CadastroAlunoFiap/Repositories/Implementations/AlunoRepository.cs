using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AlunoRepository : IAlunoRepository
{
    private readonly string _connectionString;

    public AlunoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Aluno>("SELECT * FROM Aluno WHERE Status = 1");
    }

    public async Task<Aluno?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Aluno>(
            "SELECT * FROM Aluno WHERE Id = @Id AND Status = 1", new { Id = id });
    }

    public async Task<Aluno?> GetByUsuarioAsync(string usuario)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Aluno>(
            "SELECT * FROM Aluno WHERE Usuario = @Usuario AND Status = 1", new { Usuario = usuario });
    }

    public async Task<int> AddAsync(Aluno aluno)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "INSERT INTO Aluno (Nome, Usuario, Senha, Status) VALUES (@Nome, @Usuario, @Senha, @Status)";
        return await connection.ExecuteAsync(query, aluno);
    }

    public async Task<int> UpdateAsync(Aluno aluno)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE Aluno SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha WHERE Id = @Id";
        return await connection.ExecuteAsync(query, aluno);
    }

    public async Task<int> InactivateAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE Aluno SET Status = 0 WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
