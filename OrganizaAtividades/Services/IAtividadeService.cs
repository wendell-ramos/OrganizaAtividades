using OrganizaAtividades.DTOS.Atividade;

namespace OrganizaAtividades.Services
{
    public interface IAtividadeService
    {
        Task<List<AtividadeResponseDTO>> ListarAsync(
            AtividadeFilterDTO filtro);

        Task<AtividadeResponseDTO?> BuscarPorIdAsync(int id);

        Task<AtividadeResponseDTO> CadastrarAsync(
            AtividadeCreateDTO dto);

        Task<AtividadeResponseDTO?> AlterarAsync(
            int id,
            AtividadeUpdateDTO dto);

        Task<bool> ExcluirAsync(int id);
    }
}