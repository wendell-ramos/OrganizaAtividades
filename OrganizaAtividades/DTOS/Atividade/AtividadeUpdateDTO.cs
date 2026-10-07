using System.ComponentModel.DataAnnotations;

namespace OrganizaAtividades.DTOS.Atividade
{
    public class AtividadeUpdateDTO
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [StringLength(
            150,
            MinimumLength = 3,
            ErrorMessage = "O título deve possuir entre 3 e 150 caracteres."
        )]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(
            500,
            ErrorMessage = "A descrição deve possuir no máximo 500 caracteres."
        )]
        public string Descricao { get; set; } = string.Empty;

        public DateTime DataEntrega { get; set; }

        [Required(ErrorMessage = "O campo Prioridade é obrigatório.")]
        [RegularExpression(
            "^(Baixa|Média|Alta)$",
            ErrorMessage = "A prioridade deve ser Baixa, Média ou Alta."
        )]
        public string Prioridade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Situacao é obrigatório.")]
        [RegularExpression(
            "^(Pendente|Em andamento|Concluída)$",
            ErrorMessage = "A situação deve ser Pendente, Em andamento ou Concluída."
        )]
        public string Situacao { get; set; } = string.Empty;

        public DateTime? DataConclusao { get; set; }

        [Range(
            1,
            int.MaxValue,
            ErrorMessage = "Informe uma disciplina válida."
        )]
        public int DisciplinaId { get; set; }

        [Range(
            1,
            int.MaxValue,
            ErrorMessage = "Informe uma categoria válida."
        )]
        public int CategoriaId { get; set; }
    }
}