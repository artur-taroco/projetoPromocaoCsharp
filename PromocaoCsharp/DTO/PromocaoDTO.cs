using PromocaoCsharp.Models;

namespace PromocaoCsharp.DTO
{
    public class PromocaoDTO
    {
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<int> IdProdutos { get; set; }

    }

    public class PromocaoResponseDTO
    {
        public int Id { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<ProdutoBasicoDTO> Produtos { get; set; }

        public PromocaoResponseDTO(int id, decimal percentualDesconto, DateTime dataInicio, DateTime dataFim, List<ProdutoBasicoDTO> produtos)
        {
            this.Id = id;
            this.PercentualDesconto = percentualDesconto;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Produtos = produtos;
        }
    }

    public class PromocoesAtivasDTO
    {
        public int Id { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public PromocoesAtivasDTO(int id, decimal percentualDesconto, DateTime dataInicio, DateTime dataFim)
        {
            this.Id = id;
            this.PercentualDesconto = percentualDesconto;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
        }
    }
}
