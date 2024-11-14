using Dominio.Enums;

namespace Dominio.Entidades;

public class Venda
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public FormaDePagamento FormaDePagamento { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
    public int HistoricoCompraId { get; set; }
    public HistoricoCompra HistoricoCompra { get; set; }
    public IList<Produto> Produtos { get; set; } = new List<Produto>();
}
