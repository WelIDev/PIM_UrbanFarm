using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DesktopApp.Models;
using DesktopApp.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using CategoryAxis = OxyPlot.Axes.CategoryAxis;
using ColumnSeries = OxyPlot.Series.ColumnSeries;
using LinearAxis = OxyPlot.Axes.LinearAxis;

namespace DesktopApp.Views.Dashboards
{
    public partial class VendasPage : Page
    {
        private List<VendedorModel> _vendedores = new List<VendedorModel>();
        private List<VendaModel> _vendasTotais = new List<VendaModel>();
        private List<VendaModel> _vendasAtuais = new List<VendaModel>();
        private DateTime? _dataInicio = DateTime.Now.AddYears(-100);
        private DateTime? _dataFim = DateTime.Today;

        public VendasPage()
        {
            InitializeComponent();
            CarregarDados();
        }

        private async void CarregarDados()
        {
            await CarregarVendedores();
            CarregarVendasTotais();
            FiltrarEExibirVendas(_vendasTotais);
        }

        private async Task CarregarVendedores()
        {
            try
            {
                var apiService = new ApiService();
                var vendedorModels = await apiService.ObterVendedoresAsync(_dataInicio ?? DateTime.MinValue, _dataFim ?? DateTime.MaxValue);
                if (vendedorModels != null)
                {
                    _vendedores = vendedorModels.Select(v => new VendedorModel
                    {   
                        VendedorId = v.VendedorId,
                        NomeVendedor = v.NomeVendedor,
                        Vendas = v.Vendas.Select(venda => new VendaModel { Data = venda.Data, Valor = venda.Valor }).ToList()
                    }).ToList();
                    VendedoresCardsControl.ItemsSource = _vendedores;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar vendedores: {ex.Message}");
            }
        }

        private void ExibirGraficoVendas(List<VendaModel> vendas, string nomeVendedor = "")
        {
            var tituloGrafico = string.IsNullOrEmpty(nomeVendedor) ? "Gráfico de Vendas" : $"Gráfico de Vendas do {nomeVendedor}";

            var model = new PlotModel { Title = tituloGrafico };
            var series = new ColumnSeries
            {
                Title = "Valor das Vendas",
                FillColor = OxyColor.Parse("#3cb371"),
                TrackerFormatString = "Valor {2:C2}\nData: {1}"
            };

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
                _vendasTotais = new List<VendaModel>();
            }

            _vendasAtuais = _vendasTotais;
        }

        private void ExibirVendas_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            var vendedorId = (int)button.Tag;
            var vendedorSelecionado = _vendedores.FirstOrDefault(v => v.VendedorId == vendedorId);
            if (vendedorSelecionado == null) return;
            _vendasAtuais = vendedorSelecionado.Vendas;

            ExibirGraficoVendas(_vendasAtuais, vendedorSelecionado.NomeVendedor);
        }

        private void FiltrarPorData(List<VendaModel> vendas, DateTime dataInicio, DateTime dataFim)
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

        private void FiltrarEExibirVendas(List<VendaModel> vendas)
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
            var vendasTotais = _vendedores.SelectMany(v => v.Vendas).ToList();
            ExibirGraficoVendas(vendasTotais);
        }
    }
}
