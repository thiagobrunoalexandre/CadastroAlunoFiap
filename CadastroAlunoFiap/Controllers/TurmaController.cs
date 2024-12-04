using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TurmaController : ControllerBase
{
    private readonly TurmaService _turmaService;

    public TurmaController(TurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    // 1. Obter todas as turmas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turmas = await _turmaService.GetAllAsync();
        return Ok(turmas);
    }

    // 2. Obter turma por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var turma = await _turmaService.GetByIdAsync(id);
        if (turma == null) return NotFound("Turma não encontrada.");
        return Ok(turma);
    }

    // 3. Criar uma nova turma
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Turma turma)
    {
        try
        {
            var id = await _turmaService.CreateAsync(turma);
            return CreatedAtAction(nameof(GetById), new { id }, turma);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 4. Atualizar uma turma existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Turma turma)
    {
        try
        {
            if (id != turma.Id) return BadRequest("ID da turma inválido.");
            await _turmaService.UpdateAsync(turma);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 5. Inativar uma turma
    [HttpDelete("{id}")]
    public async Task<IActionResult> Inactivate(int id)
    {
        try
        {
            await _turmaService.InactivateAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
