using CadastroAlunoFiap.Client.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class RelacionamentoService
{
    private readonly HttpClient _httpClient;

    public RelacionamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtém todos os relacionamentos de uma turma.
    /// </summary>
    /// <param name="turmaId">ID da turma.</param>
    /// <returns>Lista de relacionamentos da turma.</returns>
    public async Task<List<RelacionamentoAlunoTurma>> GetRelacionamentosPorTurmaAsync(int turmaId)
    {
        try
        {
            var relacionamentos = await _httpClient.GetFromJsonAsync<List<RelacionamentoAlunoTurma>>($"api/relacionamento/turma/{turmaId}");
            return relacionamentos ?? new List<RelacionamentoAlunoTurma>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao buscar relacionamentos da turma {turmaId}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Obtém todos os relacionamentos de um aluno.
    /// </summary>
    /// <param name="alunoId">ID do aluno.</param>
    /// <returns>Lista de relacionamentos do aluno.</returns>
    public async Task<List<RelacionamentoAlunoTurma>> GetRelacionamentosPorAlunoAsync(int alunoId)
    {
        try
        {
            var relacionamentos = await _httpClient.GetFromJsonAsync<List<RelacionamentoAlunoTurma>>($"api/relacionamento/aluno/{alunoId}");
            return relacionamentos ?? new List<RelacionamentoAlunoTurma>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao buscar relacionamentos do aluno {alunoId}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Verifica se um relacionamento entre aluno e turma já existe.
    /// </summary>
    /// <param name="alunoId">ID do aluno.</param>
    /// <param name="turmaId">ID da turma.</param>
    /// <returns>True se o relacionamento existir, False caso contrário.</returns>
    public async Task<bool> CheckExistsAsync(int alunoId, int turmaId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<dynamic>($"api/relacionamento/exists?alunoId={alunoId}&turmaId={turmaId}");
            return response?.exists ?? false;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao verificar relacionamento: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Obtém todos os relacionamentos completos (alunos e turmas).
    /// </summary>
    /// <returns>Lista de relacionamentos completos.</returns>
    public async Task<List<RelacionamentoAlunoTurma>> GetAllRelacionamentosAsync()
    {
        try
        {
            var relacionamentos = await _httpClient.GetFromJsonAsync<List<RelacionamentoAlunoTurma>>("api/relacionamento");
            return relacionamentos ?? new List<RelacionamentoAlunoTurma>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao buscar todos os relacionamentos: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Cria ou atualiza um relacionamento entre aluno e turma.
    /// </summary>
    /// <param name="relacionamento">Objeto de relacionamento.</param>
    /// <returns>Mensagem de sucesso ou erro.</returns>
    public async Task<string> CriarOuAtualizarRelacionamentoAsync(RelacionamentoAlunoTurma relacionamento)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/RelacionamentoAlunoTurma", relacionamento);

            if (response.IsSuccessStatusCode)
            {
                return "Relacionamento criado ou atualizado com sucesso.";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $" {errorMessage}";
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao criar ou atualizar relacionamento: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Inativa um relacionamento.
    /// </summary>
    /// <param name="id">ID do relacionamento.</param>
    /// <returns>Mensagem de sucesso ou erro.</returns>
    public async Task<string> InativarRelacionamentoAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/RelacionamentoAlunoTurma/relacionamento/{id}");

            if (response.IsSuccessStatusCode)
            {
                return "Relacionamento inativado com sucesso.";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $" {errorMessage}";
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao inativar relacionamento com ID {id}: {ex.Message}", ex);
        }
    }
    /// <summary>
    /// Obtém todos os alunos relacionados a uma turma.
    /// </summary>
    /// <param name="turmaId">ID da turma.</param>
    /// <returns>Lista de alunos relacionados à turma.</returns>
    public async Task<List<Aluno>> GetAlunosByTurmaIdAsync(int turmaId)
    {
        try
        {
            var alunos = await _httpClient.GetFromJsonAsync<List<Aluno>>($"api/RelacionamentoAlunoTurma/turma/{turmaId}");
            return alunos ?? new List<Aluno>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Erro ao buscar alunos relacionados à turma {turmaId}: {ex.Message}", ex);
        }
    }

}
