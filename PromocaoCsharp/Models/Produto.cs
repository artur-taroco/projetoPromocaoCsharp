using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromocaoCsharp.Models
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public List<Promocao> Promocoes { get; set; } = new List<Promocao>();

        public Produto (string nome, string descricao, decimal preco)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }

        private Produto () { }
    }
}
