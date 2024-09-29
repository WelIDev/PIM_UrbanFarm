

using Newtonsoft.Json;

namespace Dominio.Entidades;

public class Endereco
{
    public int Id { get; set; }
    [JsonProperty("logradouro")] public string Rua { get; set; }
    [JsonProperty("bairro")] public string Bairro { get; set; }
    [JsonProperty("localidade")] public string Cidade { get; set; }
    [JsonProperty("uf")] public string Estado { get; set; }
    [JsonProperty("cep")] public string Cep { get; set; }
}