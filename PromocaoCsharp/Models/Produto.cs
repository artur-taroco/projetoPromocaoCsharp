using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PromocaoCsharp.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public List<string> Imagens { get; set; }
        public List<Promocao> Promocoes { get; set; } = new List<Promocao>();

        public Produto(string nome, string descricao, decimal preco, int estoque, int categoriaId, List<string> imagens)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Estoque = estoque;
            this.CategoriaId = categoriaId;
            this.Imagens = imagens;
        }

        private Produto () { }
    }
}
