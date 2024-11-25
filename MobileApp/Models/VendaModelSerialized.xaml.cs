namespace MobileApp.Models
{
    public class VendaModelSerialized
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int VendedorId { get; set; }
        public int HistoricoCompraId { get; set; }
        public int FormaDePagamento { get; set; }
        public List<ProdutoModelSerialized> VendaProdutos { get; set; } = [];
    }

    public class ProdutoModelSerialized
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
    }
}
