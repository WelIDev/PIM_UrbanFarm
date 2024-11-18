using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using CategoryAxis = OxyPlot.Axes.CategoryAxis;
using ColumnSeries = OxyPlot.Series.ColumnSeries;
using LinearAxis = OxyPlot.Axes.LinearAxis;

namespace DesktopApp.Dashboards
{
    public partial class VendasPage
    {
        private List<Vendedor> _vendedores = [];
        private List<Venda> _vendasTotais = [];
        private List<Venda> _vendasAtuais = [];
        private DateTime? _dataInicio;
        private DateTime? _dataFim = DateTime.Today;

        public VendasPage()
        {
            InitializeComponent();
            CarregarVendedores();
            CarregarVendasTotais();
            FiltrarEExibirVendas(_vendasTotais);
        }

        private void CarregarVendedores()
        {
            // Substitua por sua chamada à API para carregar os dados dos vendedores
            _vendedores = new List<Vendedor>
            {
                new Vendedor
                {
                    VendedorId = 1, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-4), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 2, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 700 },
                        new Venda { Data = DateTime.Now.AddDays(-1), Valor = 400 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 3, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-4), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 4, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 700 },
                        new Venda { Data = DateTime.Now.AddDays(-2), Valor = 400 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 5, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-5), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 6, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 700 },
                        new Venda { Data = DateTime.Now.AddDays(-3), Valor = 400 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 7, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-4), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 8, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 7000 },
                        new Venda { Data = DateTime.Now.AddDays(-12), Valor = 400 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 7, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-4), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 8, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 7000 },
                        new Venda { Data = DateTime.Now.AddDays(-12), Valor = 400 }
                    }
                }
            };

            VendedoresCardsControl.ItemsSource = _vendedores;
        }

        private void ExibirGraficoVendas(List<Venda> vendas, string nomeVendedor = "")
        {
            var tituloGrafico = string.IsNullOrEmpty(nomeVendedor)
                ? "Gráfico de Vendas"
                : $"Gráfico de Vendas do {nomeVendedor}";

            var model = new PlotModel { Title = tituloGrafico };
            var series = new ColumnSeries
            {
                Title = "Valor das Vendas",
                FillColor = OxyColor.Parse("#3cb371"),
                TrackerFormatString = "Valor {2:C2}\nData: {1}"
            };
            // Obter a data mínima e máxima das vendas
            var dataInicio = vendas.Min(v => v.Data).Date;
            var dataFim = vendas.Max(v => v.Data).Date;

            // Cria uma lista de datas para preencher todos os dias no intervalo
            var datas = Enumerable.Range(0, (dataFim - dataInicio).Days + 1)
                .Select(offset => dataInicio.AddDays(offset))
                .ToList();

            // Adicionar itens ao gráfico, preenchendo os dias sem vendas com valores zero
            foreach (var data in datas)
            {
                var vendasDoDia = vendas.Where(v => v.Data.Date == data).Sum(v => v.Valor);
                series.Items.Add(new ColumnItem { Value = vendasDoDia });
            }

            var eixoX = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Data",
                IsZoomEnabled = false
            };

            eixoX.Labels.Clear();
            foreach (var data in datas)
            {
                eixoX.Labels.Add(data.ToString("dd/MM"));
            }

            var eixoY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Valor (R$)",
                Minimum = 0,
                MaximumPadding = 0.2,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IsZoomEnabled = false
            };

            model.Series.Add(series);
            model.Axes.Add(eixoX);
            model.Axes.Add(eixoY);

            var plotView = (PlotView)FindName("GraficoVendas");
            plotView.Model = model;
        }


        private void CarregarVendasTotais()
        {
            if (_vendedores.Count != 0)
            {
                _vendasTotais = _vendedores.SelectMany(v => v.Vendas).ToList();
            }
            else
            {
                MessageBox.Show("Nenhum Vendedor cadastrado com vendas.");
                _vendasTotais = [];
            }

            _vendasAtuais = _vendasTotais;
        }

        private void ExibirVendas_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            var vendedorId = (int)button.Tag;
            var vendedorSelecionado =
                _vendedores.FirstOrDefault(v => v.VendedorId == vendedorId);
            if (vendedorSelecionado == null) return;
            _vendasAtuais = vendedorSelecionado.Vendas;

            ExibirGraficoVendas(_vendasAtuais, vendedorSelecionado.NomeVendedor);
        }

        private void FiltrarPorData(List<Venda> vendas, DateTime dataInicio, DateTime dataFim)
        {
            var vendasFiltradas = vendas
                .Where(v => v.Data.Date >= dataInicio.Date && v.Data.Date <= dataFim.Date)
                .ToList();

            if (vendasFiltradas.Count != 0)
            {
                ExibirGraficoVendas(vendasFiltradas);
            }
            else
            {
                MessageBox.Show("Não há vendas no intervalo de datas selecionado.");
            }
        }

        private void DataInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _dataInicio = DataInicio.SelectedDate;
            FiltrarEExibirVendas(_vendasAtuais);
        }

        private void DataFim_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _dataFim = DataFim.SelectedDate;
            FiltrarEExibirVendas(_vendasAtuais);
        }

        private void FiltrarEExibirVendas(List<Venda> vendas)
        {
            if (_dataInicio.HasValue && _dataFim.HasValue)
            {
                if (_dataInicio.Value <= _dataFim.Value)
                {
                    FiltrarPorData(vendas, _dataInicio.Value, _dataFim.Value);
                }
                else
                {
                    MessageBox.Show("A data final não pode ser menor que a data inicial!");
                }
            }
            else
            {
                ExibirGraficoVendas(vendas);
            }
        }

        private void MostrarTodasVendas_Click(object sender, RoutedEventArgs e)
        {
            var vendasTotais = new List<Venda>();
            foreach (var vendedor in _vendedores)
            {
                vendasTotais.AddRange(vendedor.Vendas);
            }
            
            ExibirGraficoVendas(vendasTotais);
        }
    }

    public class Vendedor
    {
        public int VendedorId { get; set; }
        public string NomeVendedor { get; set; }
        public List<Venda> Vendas { get; set; }
    }

    public class Venda
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}
