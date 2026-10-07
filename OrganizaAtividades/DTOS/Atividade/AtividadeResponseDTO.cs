using OrganizaAtividades.DTOS.Categoria;
using OrganizaAtividades.DTOS.Disciplina;

namespace OrganizaAtividades.DTOS.Atividade
{
    public class AtividadeResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
        public string Prioridade { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public DateTime? DataConclusao { get; set; }
        public DisciplinaResponseDTO? Disciplina { get; set; }
        public CategoriaResponseDTO? Categoria { get; set; }
    }
}