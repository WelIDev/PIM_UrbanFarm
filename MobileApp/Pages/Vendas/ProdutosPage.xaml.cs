using System.Collections.ObjectModel;
using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Vendas
{
    public partial class ProdutosPage
    {
        private static ObservableCollection<ProdutoModel> Produtos { get; set; } = [];
        private readonly CartService _cartService;
        private readonly ApiService _apiService;
        private static bool _produtosCarregados;

        public ProdutosPage()
        {
            InitializeComponent();
            _apiService = new ApiService();

            _cartService = CartService.Instance;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_cartService.ProdutosCarregados)
            {
                await CarregarProdutos();
                _cartService.ProdutosCarregados = true;
            }
            else
            {
                AtualizarQuantidades(_cartService.Carrinho);
                ProdutosCollectionView.ItemsSource = Produtos; // Garante que a lista é exibida
            }
        }

        private async Task CarregarProdutos()
        {
            var apiUrl = "/Produto/ObterProdutos";
            try
            {
                var produtos = await _apiService.GetProdutosAsync(apiUrl);
                Produtos.Clear();
                foreach (var produto in produtos)
                {
                    Produtos.Add(produto);
                }

                ProdutosCollectionView.ItemsSource = Produtos;
                AtualizarQuantidades(_cartService.Carrinho);
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void AtualizarTotais()
        {
            double total = Produtos.Sum(p => p.Preco * p.Quantidade);
            CartButton.Text = $"Carrinho  -  Total: {total:C2}";
        }

        private void AtualizarQuantidades(ObservableCollection<ProdutoModel> carrinho)
        {
            foreach (var produto in carrinho)
            {
                var produtoCarrinho = Produtos.FirstOrDefault(p => p.Id == produto.Id);
                produto.Quantidade = produtoCarrinho?.Quantidade ?? 0;
            }

            ProdutosCollectionView.ItemsSource = null;
            ProdutosCollectionView.ItemsSource = Produtos;
            AtualizarTotais();
        }

        private void OnRemoveItemClicked(object sender, EventArgs e)
        {
            if (sender is not Button { CommandParameter: ProdutoModel produto }) return;
            if (produto.Quantidade <= 0) return;
            produto.Quantidade--;
            AtualizarTotais();
        }

        private void OnAddItemClicked(object sender, EventArgs e)
        {
            if (sender is not Button { CommandParameter: ProdutoModel produto }) return;
            produto.Quantidade++;
            AtualizarTotais();
        }

        private async void OnCartClicked(object sender, EventArgs e)
        {
            _cartService.Carrinho.Clear();
            foreach (var produto in Produtos.Where(p => p.Quantidade > 0))
            {
                _cartService.Carrinho.Add(produto);
            }

            await Shell.Current.GoToAsync("CartPage");
        }

        private async void OnDescricaoTapped(object sender, TappedEventArgs e)
        {
            if (sender is not Label label ||
                label.GestureRecognizers[0] is not TapGestureRecognizer tapGestureRecognizer)
                return;
            var descricaoCompleta = tapGestureRecognizer.CommandParameter as string;
            await DisplayAlert("Descrição Completa", descricaoCompleta, "OK");
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//HomePage");
            return true;
        }
    }
}
