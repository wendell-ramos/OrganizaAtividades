using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Data;
using OrganizaAtividades.DTOS.Disciplina;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly AppDbContext _context;

        public DisciplinaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisciplinaResponseDTO>> ListarAsync()
        {
            return await _context.Disciplinas
                .Select(d => new DisciplinaResponseDTO
                {
                    Id = d.Id,
                    Nome = d.Nome,
                    Descricao = d.Descricao
                })
                .ToListAsync();
        }

        public async Task<DisciplinaResponseDTO?> BuscarPorIdAsync(int id)
        {
            return await _context.Disciplinas
                .Where(d => d.Id == id)
                .Select(d => new DisciplinaResponseDTO
                {
                    Id = d.Id,
                    Nome = d.Nome,
                    Descricao = d.Descricao
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DisciplinaResponseDTO> CadastrarAsync(
            DisciplinaCreateDTO dto)
        {
            var existe = await _context.Disciplinas
                .AnyAsync(d => d.Nome == dto.Nome);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma disciplina com este nome.");
            }

            var disciplina = new Disciplina
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(disciplina.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a disciplina cadastrada.");
        }

        public async Task<DisciplinaResponseDTO?> AlterarAsync(
            int id,
            DisciplinaUpdateDTO dto)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return null;
            }

            var existe = await _context.Disciplinas
                .AnyAsync(d =>
                    d.Nome == dto.Nome &&
                    d.Id != id);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma disciplina com este nome.");
            }

            disciplina.Nome = dto.Nome;
            disciplina.Descricao = dto.Descricao;

            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(disciplina.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a disciplina alterada.");
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return false;
            }

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}