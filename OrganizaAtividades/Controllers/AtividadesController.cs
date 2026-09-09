using Microsoft.AspNetCore.Mvc;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadesController : ControllerBase
    {
        private static List<Atividade> _atividades = new List<Atividade>()
        {
            new Atividade
            {
                Id = 1,
                Titulo = "Projeto de API REST",
                Descricao = "Desenvolver o primeiro checkpoint da API",
                DataEntrega = new DateTime(2026, 9, 30),
                Prioridade = "Alta",
                Situacao = "Em andamento",
                DataConclusao = null,
                DisciplinaId = 1,
                CategoriaId = 1
            },
            new Atividade
            {
                Id = 2,
                Titulo = "Exercícios de modelagem",
                Descricao = "Finalizar os exercícios de banco de dados",
                DataEntrega = new DateTime(2026, 10, 10),
                Prioridade = "Média",
                Situacao = "Pendente",
                DataConclusao = null,
                DisciplinaId = 2,
                CategoriaId = 1
            },
            new Atividade
            {
                Id = 3,
                Titulo = "Prova de banco de dados",
                Descricao = "Revisar o conteúdo para a avaliação",
                DataEntrega = new DateTime(2026, 10, 20),
                Prioridade = "Alta",
                Situacao = "Pendente",
                DataConclusao = null,
                DisciplinaId = 2,
                CategoriaId = 2
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_atividades);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var atividade = _atividades.FirstOrDefault(a => a.Id == id);

            if (atividade == null)
            {
                return NotFound();
            }
            return Ok(atividade);
        }
    }
}