using Dominio.Enums;

namespace Aplicacao.DTOs;

public record VendaDto
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public double Valor { get; set; }
    public int VendedorId { get; set; }
    public int HistoricoCompraId { get; set; }
    public FormaDePagamento FormaDePagamento { get; set; }
    public List<VendaProdutoDto> VendaProdutos { get; set; }
}
public record VendaProdutoDto
{
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public double ValorTotal { get; set; }
}