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
        private PromocaoResponseDTO MapPromocaoToResponseDTO(Promocao promocao)
        {
            return new PromocaoResponseDTO(
                promocao.Id,
                promocao.PercentualDesconto,
                promocao.DataInicio,
                promocao.DataFim,
                promocao.Produtos.Select(produto => new ProdutoBasicoDTO(
                    produto.Id,
                    produto.Nome,
                    produto.Preco
                )).ToList()
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<PromocaoResponseDTO>> GetPromotions()
        {
            List<Promocao> promocoes = dbContext.Promocoes.Include(p => p.Produtos).ToList();

            List<PromocaoResponseDTO> resposta = promocoes.Select(promocao => MapPromocaoToResponseDTO(promocao)).ToList();

            return Ok(resposta);
        }

        [HttpGet("{id}")]
        public ActionResult<PromocaoResponseDTO> GetPromotionById(int id)
        {
            Promocao? promocao = dbContext.Promocoes.Include(p => p.Produtos).FirstOrDefault(p => p.Id == id);

            if (promocao == null)
            {
                return NotFound();
            }

            PromocaoResponseDTO resposta = MapPromocaoToResponseDTO(promocao);

            return Ok(resposta);
        }

        [HttpPost]
        public ActionResult<PromocaoResponseDTO> CreatePromotion(PromocaoDTO novaPromocaoDTO)
        {
            List<Produto> produtos = dbContext.Produtos.Where(p => novaPromocaoDTO.IdProdutos.Contains(p.Id)).ToList();

            Promocao novaPromocao = new Promocao(novaPromocaoDTO.PercentualDesconto, novaPromocaoDTO.DataInicio, novaPromocaoDTO.DataFim, produtos);

            dbContext.Add(novaPromocao);
            dbContext.SaveChanges();

            PromocaoResponseDTO resposta = MapPromocaoToResponseDTO(novaPromocao);

            return CreatedAtAction(nameof(CreatePromotion), resposta);
        }

        [HttpPut("{id}")]
        public ActionResult<PromocaoResponseDTO> UpdatePromotion(int id, PromocaoDTO promocaoAtualizadaDTO)
        {
            Promocao? promocaoExistente = dbContext.Promocoes.Include(p => p.Produtos).FirstOrDefault(p => p.Id == id);

            if (promocaoExistente == null)
            {
                return NotFound();
            }

            if (promocaoAtualizadaDTO.PercentualDesconto > 0)
                promocaoExistente.PercentualDesconto = promocaoAtualizadaDTO.PercentualDesconto;

            if (promocaoAtualizadaDTO.DataInicio != null)
                promocaoExistente.DataInicio = promocaoAtualizadaDTO.DataInicio;

            if (promocaoAtualizadaDTO.DataFim != null)
                promocaoExistente.DataFim = promocaoAtualizadaDTO.DataFim;

            List<Produto> produtos = dbContext.Produtos.Where(p => promocaoAtualizadaDTO.IdProdutos.Contains(p.Id)).ToList();
            promocaoExistente.Produtos = produtos;

            dbContext.SaveChanges();

            PromocaoResponseDTO resposta = MapPromocaoToResponseDTO(promocaoExistente);

            return Ok(resposta);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePromotion(int id)
        {
            Promocao? promocao = dbContext.Promocoes.FirstOrDefault(p => p.Id == id);

            if (promocao == null)
            {
                return NotFound();
            }

            dbContext.Promocoes.Remove(promocao);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
