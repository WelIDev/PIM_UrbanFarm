using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using DateTimeAxis = OxyPlot.Axes.DateTimeAxis;
using LinearAxis = OxyPlot.Axes.LinearAxis;
using LineSeries = OxyPlot.Series.LineSeries;

namespace DesktopApp.Dashboards
{
    public partial class VendasPage : Page
    {
        private List<Vendedor> _vendedores;
        private List<Venda> _vendasTotais;
        private List<Venda> _vendasAtuais;
        private DateTime? _dataInicio;
        private DateTime? _dataFim;

        public VendasPage()
        {
            InitializeComponent();
            CarregarVendedores();
            CarregarVendasTotais();
            AtualizarGraficoVendasTotais();
        }

        private async void CarregarVendedores()
        {
            // Substitua por sua chamada à API para carregar os dados dos vendedores
            _vendedores = new List<Vendedor>
            {
                new Vendedor
                {
                    VendedorId = 1, NomeVendedor = "Vendedor 1", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 500 },
                        new Venda { Data = DateTime.Now.AddDays(-1), Valor = 300 }
                    }
                },
                new Vendedor
                {
                    VendedorId = 2, NomeVendedor = "Vendedor 2", Vendas = new List<Venda>
                    {
                        new Venda { Data = DateTime.Now, Valor = 700 },
                        new Venda { Data = DateTime.Now.AddDays(-1), Valor = 400 }
                    }
                }
            };

            VendedoresCardsControl.ItemsSource = _vendedores;
        }

        private void ExibirGraficoVendas(List<Venda> vendas)
        {
            var model = new PlotModel { Title = "Gráfico de Vendas" };
            var series = new LineSeries { Title = "Valor das Vendas", MarkerType = MarkerType.Circle };

            foreach (var venda in vendas)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(venda.Data), venda.Valor));
            }

            model.Series.Add(series);
            
            model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "dd/MM/yyyy" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.2, Title = "Valor (R$)"
            });

            var plotView = (PlotView)FindName("GraficoVendas");
            if (plotView != null)
            {
                plotView.Model = model;
            }
        }

        private async void CarregarVendasTotais()
        {
            // Substitua por sua chamada à API para carregar os dados de vendas totais
            _vendasTotais = new List<Venda>
            {
                new Venda { Data = DateTime.Now, Valor = 1200 },
                new Venda { Data = DateTime.Now.AddDays(-1), Valor = 700 },
                new Venda { Data = DateTime.Now.AddDays(-2), Valor = 450 }
            };

            // Definindo vendas atuais como vendas totais inicialmente
            _vendasAtuais = _vendasTotais;
        }

        private void ExibirVendas_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var vendedorId = (int)button.Tag;
                var vendedorSelecionado = _vendedores.FirstOrDefault(v => v.VendedorId == vendedorId);
                if (vendedorSelecionado != null)
                {
                    _vendasAtuais = vendedorSelecionado.Vendas;
                    FiltrarEExibirVendas(_vendasAtuais);
                }
            }
        }

        private void AtualizarGraficoVendasTotais()
        {
            FiltrarEExibirVendas(_vendasTotais);
        }

        private void FiltrarPorData(List<Venda> vendas, DateTime dataInicio, DateTime dataFim)
        {
            var vendasFiltradas = vendas
                .Where(v => v.Data.Date >= dataInicio.Date && v.Data.Date <= dataFim.Date)
                .ToList();

            if (vendasFiltradas.Any())
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
            AtualizarFiltroDeData();
        }

        private void DataFim_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _dataFim = DataFim.SelectedDate;
            AtualizarFiltroDeData();
        }

        private void AtualizarFiltroDeData()
        {
            if (_dataInicio.HasValue && _dataFim.HasValue)
            {
                FiltrarEExibirVendas(_vendasAtuais);
            }
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
            FiltrarEExibirVendas(_vendasTotais);
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
