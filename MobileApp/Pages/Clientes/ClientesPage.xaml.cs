using System.Collections.ObjectModel;
using System.Text.Json;
using System.Web;
using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Clientes
{
    public partial class ClientesPage
    {
        private ObservableCollection<ClienteModel> Clientes { get; set; }
        private readonly ApiService _apiService;

        public ClientesPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Clientes = new ObservableCollection<ClienteModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarCliente();
        }

        private async Task CarregarCliente()
        {
            var apiUrl = "/Cliente/ObterClientes";
            try
            {
                var clientes = await _apiService.GetClientesAsync(apiUrl);
                Clientes.Clear();
                foreach (var cliente in (IEnumerable<ClienteModel>)clientes)
                {
                    Clientes.Add(cliente);
                }

                ClientesCollectionView.ItemsSource = Clientes;
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", e.Message, "Ok");
            }
        }

        private async void OnVerHistoricoCliente_Clicked(object? sender, EventArgs e)
        {
            if (sender is not BindableObject { BindingContext: ClienteModel cliente }) return;
            var clienteJson = JsonSerializer.Serialize(cliente);
            await Shell.Current.GoToAsync(
                $"HistoricoPage?clienteJson={HttpUtility.UrlEncode(clienteJson)}");
        }
    
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("///HomePage");
            return true;
        }

        private async void OnCadastrarClienteClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CadastroClientePage");
        }
    }
}
