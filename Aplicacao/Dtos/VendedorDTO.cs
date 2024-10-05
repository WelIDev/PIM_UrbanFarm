using Dominio.Entidades;

namespace Aplicacao.DTOs;

public class VendedorDTO
{
    public string Nome { get; set; }
    public double Salario { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime DataContratacao { get; set; }
    public Endereco Endereco { get; set; }
}
