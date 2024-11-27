namespace Dominio.Dtos
{
    public class VendedorDto
    {
        public int VendedorId { get; set; }
        public string NomeVendedor { get; set; }
        public List<VendaVendedorDto> Vendas { get; set; } = new List<VendaVendedorDto>();
    }

    public class VendaVendedorDto
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}
