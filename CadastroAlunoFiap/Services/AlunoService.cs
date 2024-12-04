public class AlunoService
{
    private readonly IAlunoRepository _repository;

    public AlunoService(IAlunoRepository repository)
    {
        _repository = repository;
    }

    // Obter todos os alunos
    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Obter aluno por ID
    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // Criar novo aluno
    public async Task<int> CreateAsync(Aluno aluno)
    {
        // Verificar se o usuário é único
        if (!await IsUsuarioUniqueAsync(aluno.Usuario))
        {
            throw new Exception("Usuário já existe.");
        }

        // Validar força da senha
        if (!IsPasswordStrong(aluno.Senha))
        {
            throw new Exception("Senha fraca. A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");
        }

        // Hashear a senha
        aluno.Senha = HashPassword(aluno.Senha);

        // Salvar no repositório
        return await _repository.AddAsync(aluno);
    }

    // Atualizar aluno existente
    public async Task UpdateAsync(Aluno aluno)
    {
        // Validar força da senha (caso esteja sendo alterada)
        if (!string.IsNullOrEmpty(aluno.Senha))
        {
            if (!IsPasswordStrong(aluno.Senha))
            {
                throw new Exception("Senha fraca. A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");
            }

            // Hashear a senha
            aluno.Senha = HashPassword(aluno.Senha);
        }

        var result = await _repository.UpdateAsync(aluno);
        if (result <= 0)
        {
            throw new Exception("Erro ao atualizar o aluno.");
        }
    }

    // Inativar aluno
    public async Task InactivateAsync(int id)
    {
        var result = await _repository.InactivateAsync(id);
        if (result <= 0)
        {
            throw new Exception("Erro ao inativar o aluno.");
        }
    }

    // Verificar se o usuário é único
    private async Task<bool> IsUsuarioUniqueAsync(string usuario)
    {
        var aluno = await _repository.GetByUsuarioAsync(usuario);
        return aluno == null;
    }

    // Validar força da senha
    private bool IsPasswordStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var hasUpperCase = password.Any(char.IsUpper);
        var hasLowerCase = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecialChar = password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>?".Contains(ch));
        var hasMinimumLength = password.Length >= 8;

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar && hasMinimumLength;
    }

    // Hashear a senha
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
