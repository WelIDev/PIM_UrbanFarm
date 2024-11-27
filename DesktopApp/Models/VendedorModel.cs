using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesktopApp.Models
{
    public class VendedorModel
    {
        public int VendedorId { get; set; }
        public string NomeVendedor { get; set; }
        public List<VendaModel> Vendas { get; set; } = new List<VendaModel>();
    }

    public class VendaModel
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}
