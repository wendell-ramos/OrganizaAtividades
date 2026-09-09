namespace OrganizaAtividades.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
        public string Prioridade { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public DateTime? DataConclusao { get; set; }
        public int DisciplinaId { get; set; }
        public int CategoriaId { get; set; }
    }
}