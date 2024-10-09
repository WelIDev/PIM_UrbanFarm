using System.Runtime.InteropServices.JavaScript;

namespace Dominio.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public int EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
    public HistoricoCompra HistoricoCompra { get; set; }
}