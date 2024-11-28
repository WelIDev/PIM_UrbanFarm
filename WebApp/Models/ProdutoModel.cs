namespace WebApp.Models;

public class ProdutoModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string? Icone { get; set; }
    public double ValorTotal { get; set; }
    public int Quantidade { get; set; }
    public double Preco { get; set; }
}
