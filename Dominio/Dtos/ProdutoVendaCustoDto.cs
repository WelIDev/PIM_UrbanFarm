namespace Dominio.Dtos
{
    public class ProdutoVendaCustoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal TotalVendas { get; set; }
        public decimal TotalCusto { get; set; }
        public decimal MargemLucro { get; set; }
    }
}
