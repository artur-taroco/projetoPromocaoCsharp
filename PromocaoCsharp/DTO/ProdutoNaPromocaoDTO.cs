namespace PromocaoCsharp.DTO
{
    public class ProdutoNaPromocaoDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public ProdutoNaPromocaoDTO(string id, string nome, decimal preco)
        {
            this.Id = id;
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}
