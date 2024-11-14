namespace Dominio.Entidades;

public class VendaProduto
{
    public int IdVenda { get; set; }
    public Venda Venda { get; set; }

    public int IdProduto { get; set; }
    public Produto Produto { get; set; }

    public int Quantidade { get; set; }
    public double ValorTotal { get; set; }
}
