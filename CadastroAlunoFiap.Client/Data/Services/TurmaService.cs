using CadastroAlunoFiap.Client.Data.Models;
using System.Net.Http.Json;

public class TurmaService
{
    private readonly HttpClient _httpClient;

    public TurmaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Obter todas as turmas
    public async Task<List<Turma>> GetTurmasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Turma>>("api/turma");
    }

    // Obter turma por ID
    public async Task<Turma> GetTurmaByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Turma>($"api/turma/{id}");
    }

    // Criar nova turma
    public async Task<string> CriarTurmaAsync(Turma turma)
    {
        var response = await _httpClient.PostAsJsonAsync("api/turma", turma);

        if (response.IsSuccessStatusCode)
        {
            return "Turma cadastrada com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar turma: {errorMessage}";
        }
    }

    // Atualizar turma existente
    public async Task<string> AtualizarTurmaAsync(Turma turma)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/turma/{turma.Id}", turma);

        if (response.IsSuccessStatusCode)
        {
            return "Turma atualizada com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao atualizar turma: {errorMessage}";
        }
    }

    // Inativar turma
    public async Task<string> InativarTurmaAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/turma/{id}");

        if (response.IsSuccessStatusCode)
        {
            return "Turma inativada com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao inativar turma: {errorMessage}";
        }
    }
}
