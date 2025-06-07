using PromocaoCsharp.Models;

namespace PromocaoCsharp.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public List<string> Imagens { get; set; }
    }
    public class ProdutoEditDTO
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public int? Estoque { get; set; }
        public int? CategoriaId { get; set; }
    }

    public class ProdutoBasicoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public ProdutoBasicoDTO(int id, string nome, decimal preco)
        {
            this.Id = id;
            this.Nome = nome;
            this.Preco = preco;
        }
    }

    public class ProdutoComPromocaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public List<string> Imagens { get; set; }
        public List<PromocoesAtivasDTO> Promocoes { get; set; }

        public ProdutoComPromocaoDTO(int id, string nome, string descricao, decimal preco, int estoque, int categoriaId, List<string> imagens, List<PromocoesAtivasDTO> promocoes)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Estoque = estoque;
            this.CategoriaId = categoriaId;
            this.Imagens = imagens;
            this.Promocoes = promocoes;
        }
    }
}
