using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Data;
using OrganizaAtividades.DTOS.Atividade;
using OrganizaAtividades.DTOS.Categoria;
using OrganizaAtividades.DTOS.Disciplina;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Services
{
    public class AtividadeService : IAtividadeService
    {
        private readonly AppDbContext _context;

        public AtividadeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AtividadeResponseDTO>> ListarAsync(
            AtividadeFilterDTO filtro)
        {
            var query = _context.Atividades.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Titulo))
            {
                query = query.Where(a =>
                    a.Titulo.Contains(filtro.Titulo));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Prioridade))
            {
                query = query.Where(a =>
                    a.Prioridade == filtro.Prioridade);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Situacao))
            {
                query = query.Where(a =>
                    a.Situacao == filtro.Situacao);
            }

            return await query
                .Select(a => new AtividadeResponseDTO
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descricao = a.Descricao,
                    DataEntrega = a.DataEntrega,
                    Prioridade = a.Prioridade,
                    Situacao = a.Situacao,
                    DataConclusao = a.DataConclusao,

                    Disciplina = new DisciplinaResponseDTO
                    {
                        Id = a.Disciplina.Id,
                        Nome = a.Disciplina.Nome,
                        Descricao = a.Disciplina.Descricao
                    },

                    Categoria = new CategoriaResponseDTO
                    {
                        Id = a.Categoria.Id,
                        Nome = a.Categoria.Nome,
                        Descricao = a.Categoria.Descricao
                    }
                })
                .ToListAsync();
        }

        public async Task<AtividadeResponseDTO?> BuscarPorIdAsync(int id)
        {
            return await _context.Atividades
                .Where(a => a.Id == id)
                .Select(a => new AtividadeResponseDTO
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descricao = a.Descricao,
                    DataEntrega = a.DataEntrega,
                    Prioridade = a.Prioridade,
                    Situacao = a.Situacao,
                    DataConclusao = a.DataConclusao,

                    Disciplina = new DisciplinaResponseDTO
                    {
                        Id = a.Disciplina.Id,
                        Nome = a.Disciplina.Nome,
                        Descricao = a.Disciplina.Descricao
                    },

                    Categoria = new CategoriaResponseDTO
                    {
                        Id = a.Categoria.Id,
                        Nome = a.Categoria.Nome,
                        Descricao = a.Categoria.Descricao
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AtividadeResponseDTO> CadastrarAsync(
            AtividadeCreateDTO dto)
        {
            var disciplinaExiste = await _context.Disciplinas
                .AnyAsync(d => d.Id == dto.DisciplinaId);

            if (!disciplinaExiste)
            {
                throw new InvalidOperationException(
                    "Não existe disciplina com este ID.");
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == dto.CategoriaId);

            if (!categoriaExiste)
            {
                throw new InvalidOperationException(
                    "Não existe categoria com este ID.");
            }

            var existe = await _context.Atividades
                .AnyAsync(a =>
                    a.Titulo == dto.Titulo &&
                    a.DisciplinaId == dto.DisciplinaId);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma atividade com este título nessa disciplina.");
            }

            var atividade = new Atividade
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataEntrega = dto.DataEntrega,
                Prioridade = dto.Prioridade,
                Situacao = dto.Situacao,
                DataConclusao = dto.DataConclusao,
                DisciplinaId = dto.DisciplinaId,
                CategoriaId = dto.CategoriaId
            };

            _context.Atividades.Add(atividade);
            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(atividade.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a atividade cadastrada.");
        }

        public async Task<AtividadeResponseDTO?> AlterarAsync(
            int id,
            AtividadeUpdateDTO dto)
        {
            var atividade = await _context.Atividades.FindAsync(id);

            if (atividade == null)
            {
                return null;
            }

            var disciplinaExiste = await _context.Disciplinas
                .AnyAsync(d => d.Id == dto.DisciplinaId);

            if (!disciplinaExiste)
            {
                throw new InvalidOperationException(
                    "Não existe disciplina com este ID.");
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == dto.CategoriaId);

            if (!categoriaExiste)
            {
                throw new InvalidOperationException(
                    "Não existe categoria com este ID.");
            }

            var existe = await _context.Atividades
                .AnyAsync(a =>
                    a.Titulo == dto.Titulo &&
                    a.DisciplinaId == dto.DisciplinaId &&
                    a.Id != id);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe uma atividade com este título nessa disciplina.");
            }

            atividade.Titulo = dto.Titulo;
            atividade.Descricao = dto.Descricao;
            atividade.DataEntrega = dto.DataEntrega;
            atividade.Prioridade = dto.Prioridade;
            atividade.Situacao = dto.Situacao;
            atividade.DataConclusao = dto.DataConclusao;
            atividade.DisciplinaId = dto.DisciplinaId;
            atividade.CategoriaId = dto.CategoriaId;

            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(atividade.Id)
                ?? throw new InvalidOperationException(
                    "Erro ao buscar a atividade alterada.");
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var atividade = await _context.Atividades.FindAsync(id);

            if (atividade == null)
            {
                return false;
            }

            _context.Atividades.Remove(atividade);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}