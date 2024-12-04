using CadastroAlunoFiap.Client.Data.Models;
using System.Net.Http.Json;

public class AlunoService
{
    private readonly HttpClient _httpClient;

    public AlunoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Aluno>> GetAlunosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Aluno>>("api/aluno");
    }

    public async Task<Aluno> GetAlunoByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Aluno>($"api/aluno/{id}");
    }

    public async Task<string> CriarAlunoAsync(Aluno aluno)
    {
        var response = await _httpClient.PostAsJsonAsync("api/aluno", aluno);

        if (response.IsSuccessStatusCode)
        {
            return "Aluno cadastrado com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao criar aluno: {errorMessage}";
        }
    }

    public async Task<string> AtualizarAlunoAsync(Aluno aluno)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/aluno/{aluno.Id}", aluno);

        if (response.IsSuccessStatusCode)
        {
            return "Aluno atualizado com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao atualizar aluno: {errorMessage}";
        }
    }

    public async Task<string> InativarAlunoAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/aluno/{id}");

        if (response.IsSuccessStatusCode)
        {
            return "Aluno inativado com sucesso.";
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"Erro ao inativar aluno: {errorMessage}";
        }
    }
}
