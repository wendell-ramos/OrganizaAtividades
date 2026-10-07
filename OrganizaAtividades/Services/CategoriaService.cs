using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Data;
using OrganizaAtividades.DTOS.Categoria;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaResponseDTO>> ListarAsync()
        {
            return await _context.Categorias
                .Select(c => new CategoriaResponseDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                })
                .ToListAsync();
        }

        public async Task<CategoriaResponseDTO?> BuscarPorIdAsync(int id)
        {
            return await _context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaResponseDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategoriaResponseDTO> CadastrarAsync(
            CategoriaCreateDTO dto)
        {
            var existe = await _context.Categorias
                .AnyAsync(c => c.Nome == dto.Nome);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma categoria com este nome.");
            }

            var categoria = new Categoria
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(categoria.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a categoria cadastrada.");
        }

        public async Task<CategoriaResponseDTO?> AlterarAsync(
            int id,
            CategoriaUpdateDTO dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return null;
            }

            var existe = await _context.Categorias
                .AnyAsync(c =>
                    c.Nome == dto.Nome &&
                    c.Id != id);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma categoria com este nome.");
            }

            categoria.Nome = dto.Nome;
            categoria.Descricao = dto.Descricao;

            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(categoria.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a categoria alterada.");
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return false;
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}