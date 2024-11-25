using System.Text.Json.Serialization;

namespace MobileApp.Models;

public class ClienteModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    public string Icone { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("cpfCnpj")]
    public string CpfCnpj { get; set; }
    [JsonPropertyName("telefone")]
    public string Telefone { get; set; }
    [JsonPropertyName("dataNasciomento")]
    public DateTime DataNascimento { get; set; }
    public EnderecoModel Endereco { get; set; }

}

public class EnderecoModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("numero")]
    public int Numero { get; set; }
    [JsonPropertyName("rua")]
    public string Rua { get; set; }
    [JsonPropertyName("bairro")]
    public string Bairro { get; set; }
    [JsonPropertyName("cidade")]
    public string Cidade { get; set; }
    [JsonPropertyName("estado")]
    public string Estado { get; set; }
    [JsonPropertyName("cep")]
    public string Cep { get; set; }
}
