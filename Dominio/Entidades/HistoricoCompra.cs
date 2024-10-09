namespace Dominio.Entidades;

public class HistoricoCompra
{
    public int Id { get; set; }
    public IList<Venda> Vendas { get; set; } = new List<Venda>();
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
}
