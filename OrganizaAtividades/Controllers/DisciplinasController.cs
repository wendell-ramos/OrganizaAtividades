using Microsoft.AspNetCore.Mvc;
using OrganizaAtividades.DTOS.Disciplina;
using OrganizaAtividades.DTOS.Validation;
using OrganizaAtividades.Services;

namespace OrganizaAtividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinasController : ControllerBase
    {
        private readonly IDisciplinaService _service;

        public DisciplinasController(IDisciplinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var disciplinas = await _service.ListarAsync();

            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var disciplina = await _service.BuscarPorIdAsync(id);

            if (disciplina == null)
            {
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Disciplina não encontrada."
                });
            }

            return Ok(disciplina);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] DisciplinaCreateDTO disciplina)
        {
            try
            {
                var disciplinaResponseDTO =
                    await _service.CadastrarAsync(disciplina);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = disciplinaResponseDTO.Id },
                    disciplinaResponseDTO
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(
            int id,
            [FromBody] DisciplinaUpdateDTO disciplinaAtualizada)
        {
            try
            {
                var disciplina = await _service.AlterarAsync(
                    id,
                    disciplinaAtualizada
                );

                if (disciplina == null)
                {
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Disciplina não encontrada."
                    });
                }

                return Ok(disciplina);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var excluiu = await _service.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}