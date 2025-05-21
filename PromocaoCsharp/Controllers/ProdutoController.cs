using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("view")]
        public ActionResult<IEnumerable<Produto>> getProducts()
        {
            return Ok(dbContext.Produtos);
        }

        [HttpPost("new")]
        public ActionResult<Produto> createProduct(ProdutoDTO novoProdutoDTO)
        {
            Produto novoProduto = new Produto(novoProdutoDTO.Nome, novoProdutoDTO.Descricao, novoProdutoDTO.Preco);

            dbContext.Produtos.Add(novoProduto);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(createProduct), novoProduto);
        }
    }
}
