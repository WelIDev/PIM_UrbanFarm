using System.Text.Json.Serialization;

namespace MobileApp.Models;

public class UsuarioModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    [JsonPropertyName("funcao")]
    public int Funcao { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
}
