using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RelacionamentoAlunoTurmaController : ControllerBase
{
    private readonly RelacionamentoService _relacionamentoService;

    public RelacionamentoAlunoTurmaController(RelacionamentoService relacionamentoService)
    {
        _relacionamentoService = relacionamentoService;
    }

    /// <summary>
    /// Cria ou atualiza um relacionamento entre aluno e turma.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromBody] RelacionamentoAlunoTurma relacionamento)
    {
        try
        {
            await _relacionamentoService.CreateOrUpdateRelacionamentoAsync(relacionamento);
            return Ok("Relacionamento criado ou atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar ou atualizar o relacionamento: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém todos os relacionamentos de uma turma.
    /// </summary>
    [HttpGet("turma/{turmaId}")]
    public async Task<IActionResult> GetByTurmaId(int turmaId)
    {
        var alunosRelacionados = await _relacionamentoService.GetAlunosByTurmaIdAsync(turmaId);
       

        return Ok(alunosRelacionados);
    }

    /// <summary>
    /// Obtém todos os relacionamentos de um aluno.
    /// </summary>
    [HttpGet("aluno/{alunoId}")]
    public async Task<IActionResult> GetByAlunoId(int alunoId)
    {
        var relacionamentos = await _relacionamentoService.GetRelacionamentosPorAlunoAsync(alunoId);
        if (relacionamentos == null || !relacionamentos.Any())
        {
            return NotFound("Nenhuma turma encontrada para este aluno.");
        }

        return Ok(relacionamentos);
    }

    /// <summary>
    /// Verifica se um relacionamento entre aluno e turma já existe.
    /// </summary>
    [HttpGet("exists")]
    public async Task<IActionResult> CheckExists([FromQuery] int alunoId, [FromQuery] int turmaId)
    {
        try
        {
            var exists = await _relacionamentoService.ExistsRelacionamentoAsync(alunoId, turmaId);
            return Ok(new { exists });
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao verificar o relacionamento: {ex.Message}");
        }
    }

    /// <summary>
    /// Inativa um relacionamento entre aluno e turma.
    /// </summary>
    [HttpDelete("relacionamento/{id}")]
    public async Task<IActionResult> Inactivate(int id)
    {
        try
        {
            await _relacionamentoService.InactivateRelacionamentoAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao inativar o relacionamento: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém todos os relacionamentos completos, incluindo informações de alunos e turmas.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllRelacionamentos()
    {
        try
        {
            var relacionamentos = await _relacionamentoService.GetRelacionamentosPorTurmaAsync(0); // Retorna todos os relacionamentos
            if (relacionamentos == null || !relacionamentos.Any())
            {
                return NotFound("Nenhum relacionamento encontrado.");
            }

            return Ok(relacionamentos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter relacionamentos: {ex.Message}");
        }
    }

     

}
