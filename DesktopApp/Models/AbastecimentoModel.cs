using System.Text.Json.Serialization;

namespace DesktopApp.Models
{
    public class AbastecimentoModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("produtos")]
        public List<ItemAbastecimentoModel> Produtos { get; set; }
        [JsonPropertyName("fornecedorId")]
        public int FornecedorId { get; set; }
        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }
        [JsonPropertyName("observacoes")]
        public string Observacoes { get; set; }
    }
}
