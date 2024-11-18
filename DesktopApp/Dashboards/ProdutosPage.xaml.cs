using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DesktopApp.Dashboards
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
            PreencherGraficoVendasPorProduto();
            PreencherGraficoTendenciaVendas();
            PreencherNiveisEstoque();
            PreencherGraficoMargemLucro();
            PreencherLancamentosRecentes();
        }

        private void PreencherProdutosMaisVendidos()
        {
            var produtosMaisVendidos = new List<Produto>
            {
                new Produto { Nome = "Produto A", Vendas = 150 },
                new Produto { Nome = "Produto B", Vendas = 120 },
                new Produto { Nome = "Produto C", Vendas = 100 },
                new Produto { Nome = "Produto D", Vendas = 150 },
                new Produto { Nome = "Produto E", Vendas = 120 },
                new Produto { Nome = "Produto F", Vendas = 100 },
                new Produto { Nome = "Produto G", Vendas = 150 },
                new Produto { Nome = "Produto H", Vendas = 120 },
                new Produto { Nome = "Produto I", Vendas = 100 }
            };

            ProdutosMaisVendidos.ItemsSource = produtosMaisVendidos.Select(p => p.Nome);
        }

        private void PreencherGraficoVendasPorProduto()
        {
            var model = new PlotModel { Title = "Vendas por Produto" };
            var series = new ColumnSeries
                { Title = "Vendas", FillColor = OxyColor.Parse("#3cb371") };

            var produtos = new List<Produto>
            {
                new Produto { Nome = "Produto A", Vendas = 150 },
                new Produto { Nome = "Produto B", Vendas = 120 },
                new Produto { Nome = "Produto C", Vendas = 100 }
            };

            foreach (var produto in produtos)
            {
                series.Items.Add(new ColumnItem { Value = produto.Vendas });
            }

            model.Series.Add(series);
            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "Produtos",
                ItemsSource = produtos,
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

            GraficoVendasPorProduto.Model = model;
        }

        private void PreencherGraficoTendenciaVendas()
        {
            var model = new PlotModel { Title = "Desempenho Mensal de Vendas" };
            var series = new LineSeries { Title = "Vendas", Color = OxyColor.Parse("#172031")};

            var vendasPorMes = new List<Venda>
            {
                new Venda { Data = new DateTime(2024, 1, 1), Valor = 50 },
                new Venda { Data = new DateTime(2024, 2, 1), Valor = 60 },
                new Venda { Data = new DateTime(2024, 3, 1), Valor = 70 },
                new Venda { Data = new DateTime(2024, 4, 1), Valor = 80 },
                new Venda { Data = new DateTime(2024, 5, 1), Valor = 40 }
            };

            foreach (var venda in vendasPorMes)
            {
                series.Points.Add(DateTimeAxis.CreateDataPoint(venda.Data, venda.Valor));
            }

            model.Series.Add(series);
            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "MMM",
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

        private void PreencherNiveisEstoque()
        {
            var niveisEstoque = new List<Produto>
            {
                new Produto { Nome = "Produto A", Estoque = 30 },
                new Produto { Nome = "Produto B", Estoque = 20 },
                new Produto { Nome = "Produto C", Estoque = 50 }
            };

            NiveisEstoque.ItemsSource =
                niveisEstoque.Select(p => $"{p.Nome}: {p.Estoque} unidades");
        }

        private void PreencherGraficoMargemLucro()
        {
            var model = new PlotModel { Title = "Margem de Lucro por Produto" };
            var series = new ColumnSeries
                { Title = "Lucro", FillColor = OxyColor.Parse("#3cb371") };

            var produtos = new List<Produto>
            {
                new Produto { Nome = "Produto A", Lucro = 500 },
                new Produto { Nome = "Produto B", Lucro = 300 },
                new Produto { Nome = "Produto C", Lucro = 700 }
            };

            foreach (var produto in produtos)
            {
                series.Items.Add(new ColumnItem { Value = produto.Lucro });
            }

            model.Series.Add(series);
            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "Produtos",
                ItemsSource = produtos,
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

        private void PreencherLancamentosRecentes()
        {
            var lancamentosRecentes = new List<Produto>
            {
                new Produto { Nome = "Produto D", DataLancamento = new DateTime(2024, 10, 1) },
                new Produto { Nome = "Produto E", DataLancamento = new DateTime(2024, 11, 15) }
            };

            LancamentosRecentes.ItemsSource = lancamentosRecentes.Select(p =>
                $"{p.Nome}: {p.DataLancamento.ToShortDateString()}");
        }
    }

    public class Produto
    {
        public string Nome { get; set; }
        public int Vendas { get; set; }
        public int Estoque { get; set; }
        public double Lucro { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
