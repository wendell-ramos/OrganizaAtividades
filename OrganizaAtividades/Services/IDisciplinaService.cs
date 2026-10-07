using OrganizaAtividades.DTOS.Disciplina;

namespace OrganizaAtividades.Services
{
    public interface IDisciplinaService
    {
        Task<List<DisciplinaResponseDTO>> ListarAsync();

        Task<DisciplinaResponseDTO?> BuscarPorIdAsync(int id);

        Task<DisciplinaResponseDTO> CadastrarAsync(
            DisciplinaCreateDTO dto);

        Task<DisciplinaResponseDTO?> AlterarAsync(
            int id,
            DisciplinaUpdateDTO dto);

        Task<bool> ExcluirAsync(int id);
    }
}