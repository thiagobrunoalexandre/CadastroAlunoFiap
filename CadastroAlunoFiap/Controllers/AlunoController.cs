using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly AlunoService _alunoService;

    public AlunoController(AlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    // 1. Obter todos os alunos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var alunos = await _alunoService.GetAllAsync();
        return Ok(alunos);
    }

    // 2. Obter aluno por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);
        if (aluno == null) return NotFound("Aluno não encontrado.");
        return Ok(aluno);
    }

    // 3. Criar um novo aluno
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Aluno aluno)
    {
        try
        {
            var id = await _alunoService.CreateAsync(aluno);
            return CreatedAtAction(nameof(GetById), new { id }, aluno);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 4. Atualizar um aluno existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Aluno aluno)
    {
        try
        {
            if (id != aluno.Id) return BadRequest("ID do aluno inválido.");
            await _alunoService.UpdateAsync(aluno);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 5. Inativar um aluno
    [HttpDelete("{id}")]
    public async Task<IActionResult> Inactivate(int id)
    {
        try
        {
            await _alunoService.InactivateAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
