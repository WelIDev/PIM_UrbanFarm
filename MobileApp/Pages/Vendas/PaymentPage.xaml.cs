using MobileApp.Models;
using MobileApp.Services;
using System.Collections.ObjectModel;

namespace MobileApp.Pages.Vendas
{
    public partial class PaymentPage
    {
        private readonly CartService _cartService;
        private readonly ApiService _apiService;
        private readonly ObservableCollection<ClienteModel> _clientes;

        public PaymentPage()
        {
            InitializeComponent();
            _cartService = CartService.Instance;
            _apiService = new ApiService();
            _clientes = new ObservableCollection<ClienteModel>();
            ClientePicker.ItemsSource = _clientes;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarClientes();
        }

        private async Task CarregarClientes()
        {
            try
            {
                var clientes = await _apiService.GetClientesAsync("/Cliente/ObterClientes");
                foreach (var cliente in clientes)
                {
                    _clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void OnFinishClicked(object? sender, EventArgs e)
        {
            if (FormaDePagamentoPicker.SelectedItem == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione uma forma de pagamento.", "OK");
                return;
            }

            if (ClientePicker.SelectedItem == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione um cliente.", "OK");
                return;
            }

            var formaDePagamento = FormaDePagamentoPicker.SelectedIndex + 1; // Ajuste conforme necessário
            var cliente = (ClienteModel)ClientePicker.SelectedItem;

            var venda = new VendaModelSerialized
            {
                Valor = _cartService.Carrinho.Sum(p => p.Preco * p.Quantidade),
                VendedorId = 2,
                HistoricoCompraId = cliente.Id,
                FormaDePagamento = formaDePagamento,
                VendaProdutos = _cartService.Carrinho.Select(p => new ProdutoModelSerialized()
                {
                    IdProduto = p.Id,
                    NomeProduto = p.Nome,
                    Quantidade = p.Quantidade,
                    ValorTotal = p.Preco * p.Quantidade
                }).ToList()

            };

            var endPoint = "/Venda/InserirVenda";
            try
            {
                var sucesso = await _apiService.InserirVendaAsync(venda, endPoint);
                if (sucesso)
                {
                    await DisplayAlert("Sucesso", "Compra finalizada com sucesso", "OK");
                    _cartService.LimparCarrinho();
                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    await DisplayAlert("Erro", "Falha ao finalizar a compra", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void OnCadastrarClienteClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CadastroClientePage");
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("CartPage");
            return true;
        }
    }
}
