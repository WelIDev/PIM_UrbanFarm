using System.Text.Json.Serialization;

namespace MobileApp.Models;

public class ClienteVendasModel
{
    public int Id { get; set; }
    public VendasContainer Vendas { get; set; }
}

public class VendasContainer
{
    [JsonPropertyName("$values")]
    public List<VendaModel> Values { get; set; }
}
