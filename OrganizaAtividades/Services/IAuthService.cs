using OrganizaAtividades.DTOS.Auth;

namespace OrganizaAtividades.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDTO?> AutenticarAsync(LoginDTO dto);
    }
}
