using System.Text.Json.Serialization;

namespace Dominio.Entidades;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }
    public string Descricao { get; set; }
    public IList<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
    [JsonIgnore]
    public IList<VendaProduto> VendaProdutos { get; set; } = new List<VendaProduto>();

    public IList<AbastecimentoEstoque> AbastecimentosEstoque { get; set; } =
        new List<AbastecimentoEstoque>();
}