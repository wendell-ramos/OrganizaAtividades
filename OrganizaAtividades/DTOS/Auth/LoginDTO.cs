using System.ComponentModel.DataAnnotations;

namespace OrganizaAtividades.DTOS.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
