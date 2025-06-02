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
        public ActionResult<IEnumerable<Promocao>> GetAll()
        {
            List<Promocao> promocoes = dbContext.Promocoes.Include(p => p.Produtos).ToList();
            List<PromocaoResponseDTO> resposta = new List<PromocaoResponseDTO>();

            foreach (Promocao promocao in promocoes)
            {
                List<ProdutoNaPromocaoDTO> produtosNaPromocao = new List<ProdutoNaPromocaoDTO>();

                foreach (Produto produto in promocao.Produtos)
                {
                    ProdutoNaPromocaoDTO produtoDTO = new ProdutoNaPromocaoDTO
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco
                    };

                    produtosNaPromocao.Add(produtoDTO);
                }

                PromocaoResponseDTO promocaoDTO = new PromocaoResponseDTO
                {
                    Id = promocao.Id,
                    PercentualDesconto = promocao.PercentualDesconto,
                    DataInicio = promocao.DataInicio,
                    DataFim = promocao.DataFim,
                    Produtos = produtosNaPromocao
                };

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
                ProdutoNaPromocaoDTO produtoDTO = new ProdutoNaPromocaoDTO
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
                };

                produtosDTO.Add(produtoDTO);
            }

            PromocaoResponseDTO resposta = new PromocaoResponseDTO

            {
                Id = novaPromocao.Id,
                PercentualDesconto = novaPromocao.PercentualDesconto,
                DataInicio = novaPromocao.DataInicio,
                DataFim = novaPromocao.DataFim,
                Produtos = produtosDTO
            };


            return CreatedAtAction(nameof(CreatePromotion), resposta);
        }
    }
}
