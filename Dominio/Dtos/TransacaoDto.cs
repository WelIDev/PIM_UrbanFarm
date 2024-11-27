namespace Dominio.Dtos;

public class TransacaoDto
{
    public DateTime Data { get; set; }
    public string Produto { get; set; }
    public decimal Valor { get; set; }
    public string Fornecedor { get; set; }
}

