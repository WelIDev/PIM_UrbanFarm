namespace Dominio.Entidades;

public class Comissao
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public DateTime Data {  get; set; }
    public int MetaId { get; set; }
    public Meta Meta { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
}
