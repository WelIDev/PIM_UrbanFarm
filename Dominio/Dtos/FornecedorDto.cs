using Dominio.Entidades;

namespace Aplicacao.DTOs;

public class FornecedorDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cnpj { get; set; }
    public string InscricaoEstadual { get; set; }
    public string Telefone { get; set; }
    public Endereco Endereco { get; set; }

}
