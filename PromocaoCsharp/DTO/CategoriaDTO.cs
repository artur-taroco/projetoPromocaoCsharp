using PromocaoCsharp.Models;

namespace PromocaoCsharp.DTO
{
    public class CategoriaDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class CategoriaEditDTO
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public List<ProdutoBasicoDTO> Produtos { get; set; }

        public CategoriaResponseDTO(int id, string nome, string descricao, bool ativo, DateTime dataCriacao, List<ProdutoBasicoDTO> produtos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Ativo = ativo;
            this.DataCriacao = dataCriacao;
            this.Produtos = produtos;
        }
    }
}
