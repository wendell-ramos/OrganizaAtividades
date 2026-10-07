using Microsoft.AspNetCore.Mvc;
using OrganizaAtividades.DTOS.Categoria;
using OrganizaAtividades.DTOS.Validation;
using OrganizaAtividades.Services;

namespace OrganizaAtividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _service.ListarAsync();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _service.BuscarPorIdAsync(id);

            if (categoria == null)
            {
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Categoria não encontrada."
                });
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CategoriaCreateDTO categoria)
        {
            try
            {
                var categoriaResponseDTO =
                    await _service.CadastrarAsync(categoria);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = categoriaResponseDTO.Id },
                    categoriaResponseDTO
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
            [FromBody] CategoriaUpdateDTO categoriaAtualizada)
        {
            try
            {
                var categoria = await _service.AlterarAsync(
                    id,
                    categoriaAtualizada
                );

                if (categoria == null)
                {
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Categoria não encontrada."
                    });
                }

                return Ok(categoria);
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