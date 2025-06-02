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

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> getProducts()
        {
            return Ok(dbContext.Produtos);
        }

        [HttpPost]
        public ActionResult<Produto> createProduct(ProdutoDTO novoProdutoDTO)
        {
            Produto novoProduto = new Produto(novoProdutoDTO.Nome, novoProdutoDTO.Descricao, novoProdutoDTO.Preco);

            dbContext.Produtos.Add(novoProduto);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(createProduct), novoProduto);
        }

        [HttpGet("ordenar/nome")]
        public ActionResult<IEnumerable<Produto>> OrderByName()
        {
            return Ok(dbContext.Produtos.OrderBy(p => p.Nome));
        }

        [HttpGet("ordenar/preco")]
        public ActionResult<IEnumerable<Produto>> OrderByPrice()
        {
            return Ok(dbContext.Produtos.OrderBy(p => p.Preco));
        }


        [HttpGet("detalhes/{id}")]
        public ActionResult<Produto> DetalhesProduto(string id)
        {
            Produto? produto = dbContext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }
    }
}
