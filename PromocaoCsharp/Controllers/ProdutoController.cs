using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromocaoCsharp.DTO;
using PromocaoCsharp.Models;

namespace PromocaoCsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly PromocaoDbContext dbContext;

        public ProdutoController(PromocaoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private ProdutoComPromocaoDTO MapProdutoToResponseDTO(Produto produto)
        {
            return new ProdutoComPromocaoDTO(
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                produto.Estoque,
                produto.CategoriaId,
                produto.Imagens,
                produto.Promocoes.Select(promocao => new PromocoesAtivasDTO(
                    promocao.Id,
                    promocao.PercentualDesconto,
                    promocao.DataInicio,
                    promocao.DataFim
                )).ToList());
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> getProducts([FromQuery] string? ordenar)
        {
            IQueryable<Produto> produtos = dbContext.Produtos.Include(p => p.Promocoes).AsQueryable();

            if (!string.IsNullOrEmpty(ordenar))
            {
                switch (ordenar.ToLower())
                {
                    case "nome":
                        produtos = produtos.OrderBy(p => p.Nome);
                        break;
                    case "preco":
                        produtos = produtos.OrderBy(p => p.Preco);
                        break;
                }
            }

            List<ProdutoComPromocaoDTO> resposta = produtos.Select(produto => MapProdutoToResponseDTO(produto)).ToList();
            return Ok(resposta);
        }

        [HttpPost]
        public ActionResult<Produto> createProduct(ProdutoDTO novoProdutoDTO)
        {
            bool categoria = dbContext.Categorias.Any(c => c.Id == novoProdutoDTO.CategoriaId);
            if (!categoria)
            {
                return BadRequest("Categoria não encontrada");
            }

            Produto novoProduto = new Produto(novoProdutoDTO.Nome, novoProdutoDTO.Descricao, novoProdutoDTO.Preco, novoProdutoDTO.Estoque, novoProdutoDTO.CategoriaId, novoProdutoDTO.Imagens);

            dbContext.Produtos.Add(novoProduto);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(createProduct), novoProduto);
        }

        [HttpGet("{id}")]
        public ActionResult<ProdutoComPromocaoDTO> DetalhesProduto(int id)
        {
            Produto? produto = dbContext.Produtos.Include(p => p.Promocoes).FirstOrDefault(p => p.Id == id); 

            if (produto == null)
            {
                return NotFound();
            }

            ProdutoComPromocaoDTO produtoDTO = MapProdutoToResponseDTO(produto);

            return Ok(produtoDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<Produto> AtualizarProduto(int id, ProdutoEditDTO produtoAtualizadoDTO)
        {
            Produto? produtoExistente = dbContext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produtoExistente == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(produtoAtualizadoDTO.Nome)) // Sem chaves pois irá executar apenas a próxima linha se for true
                produtoExistente.Nome = produtoAtualizadoDTO.Nome;

            if (!string.IsNullOrEmpty(produtoAtualizadoDTO.Descricao))
                produtoExistente.Descricao = produtoAtualizadoDTO.Descricao;

            if (produtoAtualizadoDTO.Preco.HasValue && produtoAtualizadoDTO.Preco > 0)
                produtoExistente.Preco = produtoAtualizadoDTO.Preco.Value;

            if (produtoAtualizadoDTO.Estoque.HasValue)
                produtoExistente.Estoque = produtoAtualizadoDTO.Estoque.Value;

            if (produtoAtualizadoDTO.CategoriaId.HasValue)
            {
                bool categoriaExiste = dbContext.Categorias.Any(c => c.Id == produtoAtualizadoDTO.CategoriaId.Value);
                if (!categoriaExiste)
                {
                    return BadRequest("A categoria informada não existe.");
                }
                produtoExistente.CategoriaId = produtoAtualizadoDTO.CategoriaId.Value;
            }

            dbContext.SaveChanges();

            return Ok(produtoExistente);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarProduto(int id)
        {
            Produto? produto = dbContext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            dbContext.Produtos.Remove(produto);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
