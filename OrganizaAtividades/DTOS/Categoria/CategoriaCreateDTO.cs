using System.ComponentModel.DataAnnotations;

namespace OrganizaAtividades.DTOS.Categoria
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(
            100,
            MinimumLength = 3,
            ErrorMessage = "O nome deve possuir entre 3 e 100 caracteres."
        )]
        public string Nome { get; set; } = string.Empty;

        [StringLength(
            300,
            ErrorMessage = "A descrição deve possuir no máximo 300 caracteres."
        )]
        public string Descricao { get; set; } = string.Empty;
    }
}