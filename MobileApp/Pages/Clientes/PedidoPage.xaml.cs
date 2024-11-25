using System.Text.Json;
using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Clientes
{
    [QueryProperty(nameof(VendaId), "vendaId")]
    public partial class PedidoPage
    {
        private readonly ApiService _apiService;
        private int _vendaId;
        public VendaModel Pedido { get; set; }
        private bool _isLoading;
        private bool _isButtonEnabled = true;

        public int VendaId
        {
            get => _vendaId;
            set
            {
                _vendaId = value;
                OnPropertyChanged();
                CarregarPedido().ConfigureAwait(false);
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public PedidoPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            BindingContext = this;
        }

        private async Task CarregarPedido()
        {
            if (VendaId <= 0) return;

            var endPoint = "/Venda/ObterPorId";
            try
            {
                var pedidoJson = await _apiService.GetPedidoAsync(VendaId, endPoint);
                var pedidoCompleto =
                    JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(pedidoJson);

                var itens = new List<ProdutoModel>();
                foreach (var produtoJson in pedidoCompleto["vendaProdutos"].GetProperty("$values")
                             .EnumerateArray())
                {
                    var produto = new ProdutoModel
                    {
                        Id = produtoJson.GetProperty("idProduto").GetInt32(),
                        Nome = produtoJson.GetProperty("nomeProduto").GetString(),
                        Quantidade = produtoJson.GetProperty("quantidade").GetInt32(),
                        ValorTotal = produtoJson.GetProperty("valorTotal").GetDouble()
                    };
                    itens.Add(produto);
                }

                Pedido = new VendaModel
                {
                    Id = pedidoCompleto["id"].GetInt32(),
                    DataVenda = pedidoCompleto["data"].GetDateTime(),
                    Valor = pedidoCompleto["valor"].GetDouble(),
                    FormaDePagamento = pedidoCompleto["formaDePagamento"].GetInt32(),
                    Itens = itens
                };

                OnPropertyChanged(nameof(Pedido));
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", e.Message, "Ok");
            }
        }

        private async void OnRemoveItemClicked(object? sender, EventArgs e)
        {
            IsButtonEnabled = false;
            var confirm = await DisplayAlert("Confirmação",
                "Tem certeza que deseja excluir esta venda?", "Sim", "Não");
            if (!confirm)
            {
                IsButtonEnabled = true;
                return;
            }

            IsLoading = true;

            var apiUrl = "/Venda/Excluir";
            try
            {
                var resultado = await _apiService.ExcluirVendaAsync(VendaId, apiUrl);
                if (resultado)
                {
                    await DisplayAlert("Sucesso", "Venda excluída com sucesso", "Ok");
                    await Shell.Current.GoToAsync("//ClientesPage");
                }
                else
                {
                    await DisplayAlert("Erro", "Falha ao excluir a venda", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IsLoading = false;
                IsButtonEnabled = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//ClientesPage");
            return true;
        }
    }
}
