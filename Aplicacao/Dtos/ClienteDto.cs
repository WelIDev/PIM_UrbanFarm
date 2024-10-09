using Dominio.Entidades;

namespace Aplicacao.DTOs;

public class ClienteDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public Endereco Endereco { get; set; }
}
