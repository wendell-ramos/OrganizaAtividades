using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizaAtividades.DTOS.Auth;
using OrganizaAtividades.DTOS.Validation;
using OrganizaAtividades.Services;
using System.Security.Claims;

namespace OrganizaAtividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginDTO dto)
        {
            var response = await _authService
                .AutenticarAsync(dto);

            if (response == null)
            {
                return Unauthorized(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Mensagem = "E-mail ou senha inválidos."
                });
            }

            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var id = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(id, out var usuarioId))
            {
                return Unauthorized();
            }

            return Ok(new UsuarioAutenticadoDTO
            {
                Id = usuarioId,
                Nome = User.FindFirstValue(ClaimTypes.Name)
                    ?? string.Empty,
                Email = User.FindFirstValue(ClaimTypes.Email)
                    ?? string.Empty,
                Perfil = User.FindFirstValue(ClaimTypes.Role)
                    ?? string.Empty
            });
        }
    }
}
