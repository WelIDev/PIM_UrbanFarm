namespace DesktopApp.Models;

public class FornecedorModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cnpj { get; set; }
    public string InscricaoEstadual { get; set; }
    public string Telefone { get; set; }
    public int EnderecoId { get; set; }
    public EnderecoModel Endereco { get; set; }
}
