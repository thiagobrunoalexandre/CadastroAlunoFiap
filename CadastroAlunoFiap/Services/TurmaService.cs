public class TurmaService
{
    private readonly ITurmaRepository _repository;

    public TurmaService(ITurmaRepository repository)
    {
        _repository = repository;
    }

    // Obter todas as turmas
    public async Task<IEnumerable<Turma>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Obter turma por ID
    public async Task<Turma?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // Criar nova turma
    public async Task<int> CreateAsync(Turma turma)
    {
        // Verificar se o nome da turma é único
        if (!await IsNomeUniqueAsync(turma.Nome))
        {
            throw new Exception("Já existe uma turma com este nome.");
        }

        // Salvar no repositório
        return await _repository.AddAsync(turma);
    }

    // Atualizar turma existente
    public async Task UpdateAsync(Turma turma)
    {
        // Verificar se o nome da turma é único (se foi alterado)
        var existingTurma = await _repository.GetByNomeAsync(turma.Nome);
        if (existingTurma != null && existingTurma.Id != turma.Id)
        {
            throw new Exception("Já existe uma turma com este nome.");
        }

        var result = await _repository.UpdateAsync(turma);
        if (result <= 0)
        {
            throw new Exception("Erro ao atualizar a turma.");
        }
    }

    // Inativar turma
    public async Task InactivateAsync(int id)
    {
        var result = await _repository.InactivateAsync(id);
        if (result <= 0)
        {
            throw new Exception("Erro ao inativar a turma.");
        }
    }

    // Verificar se o nome da turma é único
    private async Task<bool> IsNomeUniqueAsync(string nome)
    {
        var turma = await _repository.GetByNomeAsync(nome);
        return turma == null;
    }
}
