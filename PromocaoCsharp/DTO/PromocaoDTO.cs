using PromocaoCsharp.Models;

namespace PromocaoCsharp.DTO
{
    public class PromocaoDTO
    {
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<string> IdProdutos { get; set; }

    }
}
