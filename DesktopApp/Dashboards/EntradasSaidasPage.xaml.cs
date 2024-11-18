using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace DesktopApp.Dashboards
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
            PreencherDetalhesSaidas();
            PreencherGraficoEntradasSaidas();
        }

        private void PreencherResumoFinanceiro()
        {
            // Dados genéricos para o resumo financeiro
            double totalEntradas = 3500.00;
            double totalSaidas = 2500.00;
            double balanco = totalEntradas - totalSaidas;

            // Atualizar TextBlocks com os dados
            TotalEntradas.Text = $"Total de Entradas: R$ {totalEntradas:0.00}";
            TotalSaidas.Text = $"Total de Saídas: R$ {totalSaidas:0.00}";
            Balanco.Text = $"Balanço: R$ {balanco:0.00}";
        }

        private void PreencherDetalhesEntradas()
        {
            var entradas = new List<Transacao>
            {
                new Transacao { Data = new DateTime(2024, 1, 1), Produto = "Produto A", Valor = 1500.00, Fornecedor = "Fornecedor X" },
                new Transacao { Data = new DateTime(2024, 2, 1), Produto = "Produto B", Valor = 2000.00, Fornecedor = "Fornecedor Y" }
            };

            DetalhesEntradas.ItemsSource = entradas.Select(e => $"{e.Data.ToString("MM/yyyy")} - {e.Produto} - R$ {e.Valor:0.00} - {e.Fornecedor}");
        }

        private void PreencherDetalhesSaidas()
        {
            var saidas = new List<Transacao>
            {
                new Transacao { Data = new DateTime(2024, 1, 15), Produto = "Produto A", Valor = 1000.00, Destino = "Cliente X" },
                new Transacao { Data = new DateTime(2024, 2, 20), Produto = "Produto C", Valor = 1500.00, Destino = "Cliente Y" }
            };

            DetalhesSaidas.ItemsSource = saidas.Select(s => $"{s.Data.ToString("MM/yyyy")} - {s.Produto} - R$ {s.Valor:0.00} - {s.Destino}");
        }

        private void PreencherGraficoEntradasSaidas()
        {
            var model = new PlotModel { Title = "Entradas e Saídas ao Longo do Tempo" };
            var seriesEntradas = new ColumnSeries { Title = "Entradas", FillColor = OxyColor.Parse("#3cb371") };
            var seriesSaidas = new ColumnSeries { Title = "Saídas", FillColor = OxyColor.Parse("#f08080") };

            var movimentacoes = new List<Transacao>
            {
                new Transacao { Data = new DateTime(2024, 1, 1), Valor = 1500.00, Tipo = "Entrada" },
                new Transacao { Data = new DateTime(2024, 2, 1), Valor = 2000.00, Tipo = "Entrada" },
                new Transacao { Data = new DateTime(2024, 1, 15), Valor = 1000.00, Tipo = "Saída" },
                new Transacao { Data = new DateTime(2024, 2, 20), Valor = 1500.00, Tipo = "Saída" }
            };

            var movimentacoesPorMes = movimentacoes
                .GroupBy(m => new { m.Data.Year, m.Data.Month, m.Tipo })
                .Select(g => new { g.Key.Year, g.Key.Month, g.Key.Tipo, Valor = g.Sum(x => x.Valor) })
                .ToList();

            var categorias = movimentacoesPorMes
                .Select(m => new DateTime(m.Year, m.Month, 1).ToString("MM/yyyy"))
                .Distinct()
                .ToList();

            foreach (var movimentacao in movimentacoesPorMes)
            {
                if (movimentacao.Tipo == "Entrada")
                {
                    seriesEntradas.Items.Add(new ColumnItem { Value = movimentacao.Valor, CategoryIndex = categorias.IndexOf(new DateTime(movimentacao.Year, movimentacao.Month, 1).ToString("MM/yyyy")) });
                }
                else if (movimentacao.Tipo == "Saída")
                {
                    seriesSaidas.Items.Add(new ColumnItem { Value = movimentacao.Valor, CategoryIndex = categorias.IndexOf(new DateTime(movimentacao.Year, movimentacao.Month, 1).ToString("MM/yyyy")) });
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
    }

    public class Transacao
    {
        public DateTime Data { get; set; }
        public string Produto { get; set; }
        public double Valor { get; set; }
        public string Fornecedor { get; set; } // Para entradas
        public string Destino { get; set; } // Para saídas
        public string Tipo { get; set; } // Entrada ou Saída
    }
}
