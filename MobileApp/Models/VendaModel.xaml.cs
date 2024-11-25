using System.Text.Json.Serialization;

namespace MobileApp.Models
{
    public class VendaModel
    {
        public int Id { get; set; }
        [JsonPropertyName("data")]
        public DateTime DataVenda { get; set; }
        public double Valor { get; set; }
        public int FormaDePagamento { get; set; }
        public List<ProdutoModel> Itens { get; set; } = [];
    }
}
