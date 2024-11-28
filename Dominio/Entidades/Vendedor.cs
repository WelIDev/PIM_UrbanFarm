namespace Dominio.Entidades;

public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Salario { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime DataContratacao { get; set; }
    public int EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
    public IList<Venda> Vendas { get; set; }    
}
