using Microsoft.AspNetCore.Mvc;
using OrganizaAtividades.DTOS.Atividade;
using OrganizaAtividades.DTOS.Validation;
using OrganizaAtividades.Services;

namespace OrganizaAtividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadeService _service;

        public AtividadesController(IAtividadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] AtividadeFilterDTO filtro)
        {
            var atividades = await _service.ListarAsync(filtro);

            return Ok(atividades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var atividade = await _service.BuscarPorIdAsync(id);

            if (atividade == null)
            {
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Atividade não encontrada."
                });
            }

            return Ok(atividade);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] AtividadeCreateDTO atividade)
        {
            try
            {
                var atividadeResponseDTO =
                    await _service.CadastrarAsync(atividade);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = atividadeResponseDTO.Id },
                    atividadeResponseDTO
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
            [FromBody] AtividadeUpdateDTO atividadeAtualizada)
        {
            try
            {
                var atividade = await _service.AlterarAsync(
                    id,
                    atividadeAtualizada
                );

                if (atividade == null)
                {
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Atividade não encontrada."
                    });
                }

                return Ok(atividade);
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