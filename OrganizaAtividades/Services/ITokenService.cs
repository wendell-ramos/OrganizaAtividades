using OrganizaAtividades.DTOS.Auth;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Services
{
    public interface ITokenService
    {
        TokenResponseDTO GerarToken(Usuario usuario);
    }
}
