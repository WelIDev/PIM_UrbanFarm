namespace DesktopApp.Models;

public class TransacaoModel
{
    public DateTime Data { get; set; }
    public string Produto { get; set; }
    public decimal Valor { get; set; }
    public string Fornecedor { get; set; }
}
