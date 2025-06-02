namespace PromocaoCsharp.DTO
{
    public class PromocaoResponseDTO
    {
        public int Id { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<ProdutoNaPromocaoDTO> Produtos { get; set; }

        public PromocaoResponseDTO(int id, decimal percentualDesconto, DateTime dataInicio, DateTime dataFim, List<ProdutoNaPromocaoDTO> produtos)
        {
            this.Id = id;
            this.PercentualDesconto = percentualDesconto;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Produtos = produtos;
        }
    }
}
