using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromocaoCsharp.DTO;
using PromocaoCsharp.Models;

namespace PromocaoCsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private readonly PromocaoDbContext dbContext;

        public PromocaoController(PromocaoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Promocao>> GetPromotions()
        {
            List<Promocao> promocoes = dbContext.Promocoes.Include(p => p.Produtos).ToList(); // visualiza as promoções com GET

            List<PromocaoResponseDTO> resposta = new List<PromocaoResponseDTO>(); // Cria um DTO para inserir o DTO de produtos, impedindo a ref. ciclica

            foreach (Promocao promocao in promocoes)
            {
                List<ProdutoNaPromocaoDTO> produtosNaPromocao = new List<ProdutoNaPromocaoDTO>();

                foreach (Produto produto in promocao.Produtos)
                {
                    ProdutoNaPromocaoDTO produtoDTO = new ProdutoNaPromocaoDTO(produto.Id, produto.Nome, produto.Preco);

                    produtosNaPromocao.Add(produtoDTO);
                }

                PromocaoResponseDTO promocaoDTO = new PromocaoResponseDTO(promocao.Id, promocao.PercentualDesconto, promocao.DataInicio, promocao.DataFim, produtosNaPromocao);

                resposta.Add(promocaoDTO);
            }

            return Ok(resposta);
        }

        [HttpPost]
        public ActionResult<Promocao> CreatePromotion(PromocaoDTO novaPromocaoDTO)
        {
            List<Produto> produtos = dbContext.Produtos.Where(p => novaPromocaoDTO.IdProdutos.Contains(p.Id)).ToList();

            Promocao novaPromocao = new Promocao(novaPromocaoDTO.PercentualDesconto, novaPromocaoDTO.DataInicio, novaPromocaoDTO.DataFim, produtos);

            dbContext.Add(novaPromocao);
            dbContext.SaveChanges();

            List<ProdutoNaPromocaoDTO> produtosDTO = new List<ProdutoNaPromocaoDTO>();

            foreach (Produto produto in produtos)
            {
                ProdutoNaPromocaoDTO produtoDTO = new ProdutoNaPromocaoDTO(produto.Id, produto.Nome, produto.Preco);

                produtosDTO.Add(produtoDTO);
            }

            PromocaoResponseDTO resposta = new PromocaoResponseDTO(novaPromocao.Id, novaPromocao.PercentualDesconto, novaPromocao.DataInicio, novaPromocao.DataFim, produtosDTO);
            
            return CreatedAtAction(nameof(CreatePromotion), resposta);
        }
    }
}
