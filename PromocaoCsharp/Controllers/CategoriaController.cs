using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromocaoCsharp.DTO;
using PromocaoCsharp.Models;

namespace PromocaoCsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly PromocaoDbContext dbContext;

        public CategoriaController(PromocaoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaResponseDTO>> GetCategories([FromQuery] bool inativas = false)
        {
            IQueryable<Categoria> categorias = dbContext.Categorias.Include(c => c.Produtos);

            if (!inativas)
            {
                categorias = categorias.Where(c => c.Ativo);
            }

            List<CategoriaResponseDTO> resposta = categorias.Select(categoria => new CategoriaResponseDTO(
                categoria.Id,
                categoria.Nome,
                categoria.Descricao,
                categoria.Ativo,
                categoria.DataCriacao,
                categoria.Produtos.Select(produto => new ProdutoBasicoDTO(
                    produto.Id,
                    produto.Nome,
                    produto.Preco
                )).ToList()
            )).ToList();

            return Ok(resposta);
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            Categoria? categoria = dbContext.Categorias
                .Include(c => c.Produtos)  
                .FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> createCategory(CategoriaDTO novaCategoriaDTO)
        {
            bool categoriaExistente = dbContext.Categorias.Any(c => c.Nome == novaCategoriaDTO.Nome);
            if (categoriaExistente)
            {
                return BadRequest("Categoria com o mesmo nome já existe.");
            }

            Categoria novaCategoria = new Categoria(novaCategoriaDTO.Nome, novaCategoriaDTO.Descricao, true, DateTime.UtcNow);

            dbContext.Categorias.Add(novaCategoria);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetCategoria), new { id = novaCategoria.Id }, novaCategoria);
        }

        [HttpPut("{id}")]
        public ActionResult<Categoria> updateCategory(int id, CategoriaEditDTO categoriaAtualizadaDTO)
        {
            Categoria? categoriaExistente = dbContext.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoriaExistente == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(categoriaAtualizadaDTO.Nome)) // Sem chaves pois irá executar apenas a próxima linha se for true
                categoriaExistente.Nome = categoriaAtualizadaDTO.Nome;

            if (!string.IsNullOrEmpty(categoriaAtualizadaDTO.Descricao))
                categoriaExistente.Descricao = categoriaAtualizadaDTO.Descricao;

            if (categoriaAtualizadaDTO.Ativo.HasValue)
                categoriaExistente.Ativo = categoriaAtualizadaDTO.Ativo.Value;

            dbContext.SaveChanges();

            return Ok(categoriaExistente);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            Categoria? categoria = dbContext.Categorias.Include(c => c.Produtos).FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            if (categoria.Produtos.Any())
            {
                return BadRequest("Não é possível excluir a categoria. Ela contém produtos associados.");
            }

            dbContext.Categorias.Remove(categoria);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
