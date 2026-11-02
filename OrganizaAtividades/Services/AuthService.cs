using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Data;
using OrganizaAtividades.DTOS.Auth;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            AppDbContext context,
            IPasswordHasher<Usuario> passwordHasher,
            ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDTO?> AutenticarAsync(LoginDTO dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !usuario.Ativo)
            {
                return null;
            }

            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.SenhaHash,
                dto.Senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return _tokenService.GerarToken(usuario);
        }
    }
}
