using System.Windows.Controls;
using DesktopApp.Models;
using DesktopApp.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DesktopApp.Views.Dashboards
{
    public partial class ProdutosPage : Page
    {
        public ProdutosPage()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            PreencherProdutosMaisVendidos();
            PreencherGraficoTendenciaVendas();
            PreencherNiveisEstoque();
            PreencherGraficoMargemLucro();
            PreencherLancamentosRecentes();
        }

        private async void PreencherProdutosMaisVendidos()
        {
            try
            {
                var apiService = new ApiService();
                var produtosMaisVendidos = await apiService.ObterProdutosMaisVendidosAsync();

                var produtosFormatados = produtosMaisVendidos
                    .Select(p => $"{p.Nome} - {p.TotalVendido} vendidos").ToList();

                ProdutosMaisVendidos.ItemsSource = produtosFormatados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produtos mais vendidos: {ex.Message}");
            }
        }

        private async void PreencherGraficoTendenciaVendas()
        {
            try
            {
                var apiService = new ApiService();
                var vendasMensais = await apiService.ObterVendasMensaisAsync();

                var model = new PlotModel { Title = "Desempenho Mensal de Vendas" };
                var series = new LineSeries { Title = "Vendas", Color = OxyColor.Parse("#172031") };

                foreach (var venda in vendasMensais)
                {
                    var data = DateTime.ParseExact(venda.MesAno, "yyyy-MM", null);
                    series.Points.Add(DateTimeAxis.CreateDataPoint(data, (double)venda.TotalMensal));
                }

                model.Series.Add(series);
                model.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "MMM/yy",
                    IntervalType = DateTimeIntervalType.Months,
                    MinorIntervalType = DateTimeIntervalType.Days,
                    IntervalLength = 80,
                    IsZoomEnabled = false
                });
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MaximumPadding = 0.2,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false
                });

                GraficoEvolucaoVendas.Model = model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher gráfico de tendência de vendas: {ex.Message}");
            }
        }
        
        private async void PreencherNiveisEstoque()
        {
            try
            {
                var apiService = new ApiService();
                var niveisEstoque = await apiService.ObterNiveisEstoqueAsync();

                NiveisEstoque.ItemsSource =
                    niveisEstoque.Select(p => $"{p.Nome}: {p.Estoque} unidades");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar níveis de estoque: {ex.Message}");
            }
        }

        private async void PreencherGraficoMargemLucro()
        {
            try
            {
                var apiService = new ApiService();
                var produtosLucro = await apiService.ObterMargemLucroProdutosAsync();
                
                var model = new PlotModel { Title = "Margem de Lucro por Produto" };
                var series = new ColumnSeries
                {
                    Title = "Lucro",
                    FillColor = OxyColor.Parse("#3cb371")
                };

                foreach (var produto in produtosLucro)
                {
                    series.Items.Add(new ColumnItem { Value = (double)produto.MargemLucro });
                }

                model.Series.Add(series);
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "Produtos",
                    ItemsSource = produtosLucro,
                    LabelField = "Nome",
                    IsZoomEnabled = false
                });
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Minimum = 0,
                    MaximumPadding = 0.2,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false
                });

                GraficoMargemLucro.Model = model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher gráfico de margem de lucro: {ex.Message}");
            }
        }

       private async void PreencherLancamentosRecentes()
       {
           try
           {
               var apiService = new ApiService();
               var lancamentosRecentes = await apiService.ObterUltimosProdutosAsync();

               LancamentosRecentes.ItemsSource = lancamentosRecentes.Select(p =>
                   $"{p.Nome}: {p.Descricao}");
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Erro ao carregar lançamentos recentes: {ex.Message}");
           }
       }
    }
}
