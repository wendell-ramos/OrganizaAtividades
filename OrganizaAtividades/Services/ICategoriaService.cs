using OrganizaAtividades.DTOS.Categoria;

namespace OrganizaAtividades.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaResponseDTO>> ListarAsync();

        Task<CategoriaResponseDTO?> BuscarPorIdAsync(int id);

        Task<CategoriaResponseDTO> CadastrarAsync(
            CategoriaCreateDTO dto);

        Task<CategoriaResponseDTO?> AlterarAsync(
            int id,
            CategoriaUpdateDTO dto);

        Task<bool> ExcluirAsync(int id);
    }
}