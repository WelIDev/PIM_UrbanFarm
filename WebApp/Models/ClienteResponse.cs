using System.Text.Json.Serialization;

namespace WebApp.Models;

public class ClienteResponse
{
    [JsonPropertyName("$values")]
    public List<ClienteModel> Clientes { get; set; }
}
