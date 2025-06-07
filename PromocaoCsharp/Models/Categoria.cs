using System.ComponentModel.DataAnnotations;

namespace PromocaoCsharp.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria (string nome, string descricao, bool ativo, DateTime dataCriacao)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Ativo = ativo;
            this.DataCriacao = dataCriacao;
        }

        private Categoria() { }
    }
}
