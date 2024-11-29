using System.Text.Json.Serialization;

namespace WebApp.Models;

public class FornecedorModel
{
    public int Id { get; set; }

    [JsonPropertyName("Nome")]
    public string Nome { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Cnpj")]
    public string Cnpj { get; set; }

    [JsonPropertyName("InscricaoEstadual")]
    public string InscricaoEstadual { get; set; }

    [JsonPropertyName("Telefone")]
    public string Telefone { get; set; }

    [JsonPropertyName("EnderecoId")]
    public int EnderecoId { get; set; }

    [JsonPropertyName("Endereco")]
    public EnderecoModel Endereco { get; set; }
}