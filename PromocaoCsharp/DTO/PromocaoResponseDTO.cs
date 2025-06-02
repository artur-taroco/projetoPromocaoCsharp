namespace PromocaoCsharp.DTO
{
    public class PromocaoResponseDTO
    {
        public int Id { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<ProdutoNaPromocaoDTO> Produtos { get; set; }
    }
}
