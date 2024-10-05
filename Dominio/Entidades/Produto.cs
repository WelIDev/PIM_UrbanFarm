namespace Dominio.Entidades;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }
    public string Descricao { get; set; }
    public IList<Fornecedor> Fornecedores { get; set; }
    public IList<Venda> Vendas { get; set; }
}
