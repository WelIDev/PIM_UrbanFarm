using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Controls;

namespace DesktopApp.Dashboards;

public partial class VendasPage : Page
{
    private readonly HttpClient _client = new HttpClient();
    private List<Venda> _todasAsVendas = [];

    public VendasPage()
    {
        InitializeComponent();
        LoadVendas();
    }

    private async void LoadVendas()
    {
        var response = await _client.GetAsync("https://localhost:7124/api/Venda/ObterVendas");
        response.EnsureSuccessStatusCode();
        
        _todasAsVendas = await response.Content.ReadFromJsonAsync<List<Venda>>() ?? throw new InvalidOperationException();
        AtualizarDataGrid(_todasAsVendas);
    }

    private void AtualizarDataGrid(List<Venda> vendas)
    {
        VendasDataGrid.ItemsSource = vendas;
    }

    private void PeriodoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selecionado = (e.AddedItems[0] as ComboBoxItem)?.Content.ToString();
        var agora = DateTime.Now;

        var vendasFiltradas = _todasAsVendas;

        switch (selecionado)
        {
            case "Mês":
                vendasFiltradas = _todasAsVendas.Where(v => v.Data >= agora.AddMonths(-1)).ToList();
                break;
            case "Trimestre":
                vendasFiltradas = _todasAsVendas.Where(v => v.Data >= agora.AddMonths(-3)).ToList();
                break;
            case "Ano":
                vendasFiltradas = _todasAsVendas.Where(v => v.Data >= agora.AddYears(-1)).ToList();
                break;
        }
        
        AtualizarDataGrid(vendasFiltradas);
    }
}

internal record Venda
{
    public int Id { get; init; }
    public DateTime Data { get; init; }
    public double Valor { get; init; }
    public int FormaDePagamento { get; init; }
    public int VendedorId { get; init; }
    public int? HistoricoCompraId { get; init; }
    public List<Produto> Produtos { get; init; }
}

internal record Produto
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public double Preco { get; init; }
    public int Estoque { get; init; }
    public string Descricao { get; init; }
    public List<object> Fornecedores { get; init; }
    public List<object> AbastecimentosEstoque { get; init; }
}
