namespace Dominio.Entidades;

public class HistoricoCompraPaginado
{
    public HistoricoCompra Historico { get; set; }
    public int TotalRecords { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
