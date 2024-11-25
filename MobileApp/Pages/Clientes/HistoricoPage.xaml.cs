using System.Collections.ObjectModel;
using System.Text.Json;
using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Clientes
{
    [QueryProperty(nameof(ClienteJson), "clienteJson")]
    public partial class HistoricoPage
    {
        private readonly ApiService _apiService;
        private string _clienteJson;
        private const int InitialLoadCount = 10;
        private bool _isLoading;
        private bool _isDataLoaded;

        public string ClienteJson
        {
            get => _clienteJson;
            set
            {
                _clienteJson = Uri.UnescapeDataString(value);
                Cliente = JsonSerializer.Deserialize<ClienteModel>(_clienteJson);
                OnPropertyChanged(nameof(Cliente));
            }
        }

        public ClienteModel Cliente { get; set; }
        public ObservableCollection<VendaModel> Vendas { get; set; } = [];

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            set
            {
                _isDataLoaded = value;
                OnPropertyChanged();
            }
        }

        public HistoricoPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            BindingContext = this;
        }
        

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            IsLoading = true;
            IsDataLoaded = false;
            await CarregarVendas();
            IsLoading = false;
            IsDataLoaded = true;
        }

        private async Task CarregarVendas()
        {
            var apiUrl = "/HistoricoCompra/ObterHistoricoPorClienteId";
            try
            {
                var vendas = await _apiService.GetVendasAsync(Cliente.Id, apiUrl);
                Vendas.Clear();
                foreach (var venda in vendas)
                {
                    Vendas.Add(venda);
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    VendasCollectionView.ItemsSource =
                        new ObservableCollection<VendaModel>(Vendas.Take(InitialLoadCount));
                });
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Erro", e.Message, "Ok");
                });
            }
        }

        private void CarregarMaisVendas(object sender, EventArgs e)
        {
            var currentCount = ((ObservableCollection<VendaModel>)VendasCollectionView.ItemsSource).Count;
            var additionalVendas = Vendas.Skip(currentCount).Take(InitialLoadCount).ToList();
            foreach (var venda in additionalVendas)
            {
                ((ObservableCollection<VendaModel>)VendasCollectionView.ItemsSource).Add(venda);
            }
        }

        private async void OnVerPedido_Clicked(object? sender, TappedEventArgs e)
        {
            var venda = (VendaModel)e.Parameter;
            var vendaId = venda.Id;
            await Shell.Current.GoToAsync($"PedidoPage?vendaId={vendaId}");
        }
    
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("ClientesPage");
            return true;
        }
    }
}
