using DesktopApp.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DesktopApp.Views.Dashboards
{
    public partial class EntradasSaidasPage
    {
        public EntradasSaidasPage()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            PreencherResumoFinanceiro();
            PreencherDetalhesEntradas();
            PreencherGraficoEntradasSaidas();
        }

        private async void PreencherResumoFinanceiro()
        {
            try
            {
                var apiService = new ApiService();
                var resumoFinanceiro = await apiService.ObterResumoFinanceiroAsync();

                if (resumoFinanceiro != null)
                {
                    TotalEntradas.Text =
                        $"Total de Entradas: R$ {resumoFinanceiro.TotalEntradas:0.00}";
                    TotalSaidas.Text = $"Total de Saídas: R$ {resumoFinanceiro.TotalSaidas:0.00}";
                    Balanco.Text = $"Balanço: R$ {resumoFinanceiro.Balanco:0.00}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher resumo financeiro: {ex.Message}");
            }
        }

        private async void PreencherDetalhesEntradas()
        {
            try
            {
                var apiService = new ApiService();
                var entradas = await apiService.ObterDetalhesEntradasAsync();

                DetalhesEntradas.ItemsSource = entradas.Select(e =>
                    $"{e.Data.ToString("MM/yyyy")} - {e.Produto} - R$ {e.Valor:0.00} - {e.Fornecedor}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher detalhes das entradas: {ex.Message}");
            }
        }
        
        private async void PreencherGraficoEntradasSaidas()
        {
            try
            {
                var apiService = new ApiService();
                var movimentacoes = await apiService.ObterMovimentacoesMonetariasAsync();

                var model = new PlotModel { Title = "Entradas e Saídas ao Longo do Tempo" };
                var seriesEntradas = new ColumnSeries
                    { Title = "Entradas", FillColor = OxyColor.Parse("#3cb371") };
                var seriesSaidas = new ColumnSeries
                    { Title = "Saídas", FillColor = OxyColor.Parse("#f08080") };

                var movimentacoesPorMes = movimentacoes
                    .GroupBy(m => new { m.Data.Year, m.Data.Month, m.Tipo })
                    .Select(g => new
                        { g.Key.Year, g.Key.Month, g.Key.Tipo, Valor = g.Sum(x => x.Valor) })
                    .ToList();

                var categorias = movimentacoesPorMes
                    .Select(m => new DateTime(m.Year, m.Month, 1).ToString("MM/yyyy"))
                    .Distinct()
                    .ToList();

                foreach (var movimentacao in movimentacoesPorMes)
                {
                    if (movimentacao.Tipo == "Entrada")
                    {
                        seriesEntradas.Items.Add(new ColumnItem
                        {
                            Value = (double)movimentacao.Valor,
                            CategoryIndex = categorias.IndexOf(
                                new DateTime(movimentacao.Year, movimentacao.Month, 1).ToString(
                                    "MM/yyyy"))
                        });
                    }
                    if (movimentacao.Tipo == "Saida")
                    {
                        seriesSaidas.Items.Add(new ColumnItem
                        {
                            Value = (double)movimentacao.Valor,
                            CategoryIndex = categorias.IndexOf(
                                new DateTime(movimentacao.Year, movimentacao.Month, 1).ToString(
                                    "MM/yyyy"))
                        });
                    }
                }

                model.Series.Add(seriesEntradas);
                model.Series.Add(seriesSaidas);
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "Data",
                    ItemsSource = categorias,
                    IsZoomEnabled = false
                });
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MaximumPadding = 0.2,
                    IsZoomEnabled = false,
                    Minimum = 0
                });

                GraficoEntradasSaidas.Model = model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher gráfico de entradas e saídas: {ex.Message}");
            }
        }
    }
}