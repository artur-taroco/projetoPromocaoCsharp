using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromocaoCsharp.Models
{
    public class Promocao
    {
        [Key]
        public int Id { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public Promocao(decimal percentualDesconto, DateTime dataInicio, DateTime dataFim, List<Produto> produtos)
        {
            this.PercentualDesconto = percentualDesconto;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Produtos = produtos;
        }

        private Promocao() { }
    }
}
