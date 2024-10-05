using Dominio.Enums;

namespace Aplicacao.DTOs;

public record VendaDto
{
    public double Valor { get; set; }
    public int VendedorId { get; set; }
    public int ClienteId { get; set; }
    public IList<int> ProdutoIds { get; set; } = new List<int>();
    public FormaDePagamento FormaDePagamento { get; set; }
}
