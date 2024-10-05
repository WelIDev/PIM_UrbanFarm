namespace Dominio.Entidades;

public class Meta
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public int Periodo { get; set; }
    public DateTime DataCriacao { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
    public ICollection<Comissao> Comissoes { get; set; }
}
