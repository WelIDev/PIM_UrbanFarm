using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Vendas
{
    public partial class CartPage
    {
        private readonly CartService _cartService;

        public CartPage()
        {
            InitializeComponent();
            _cartService = CartService.Instance;

            CarrinhoCollectionView.ItemsSource = _cartService.Carrinho;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CalcularTotalCarrinho();
        }

        private void CalcularTotalCarrinho()
        {
            TotalCarrinhoLabel.Text = $"Total: {_cartService.Carrinho.Sum(p => p.Preco * p.Quantidade):C2}";
        }

        private void OnRemoveItemClicked(object sender, EventArgs e)
        {
            if (sender is not Button { CommandParameter: ProdutoModel produto }) return;
            _cartService.RemoverProduto(produto);
            CalcularTotalCarrinho();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//ProdutosPage");
            return true;
        }

        private async void OnFinishClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PaymentPage));
        }
    }
}
