using System.Collections.ObjectModel;
using MobileApp.Models;

namespace MobileApp.Services
{
    public class CartService
    {
        private static readonly CartService _instance = new CartService();
        public static CartService Instance => _instance;

        private ObservableCollection<ProdutoModel> _carrinho = new ObservableCollection<ProdutoModel>();
        public ObservableCollection<ProdutoModel> Carrinho => _carrinho;
        public bool ProdutosCarregados { get; set; }

        public void RemoverProduto(ProdutoModel produto)
        {
            var produtoExistente = _carrinho.FirstOrDefault(p => p.Id == produto.Id);
            if (produtoExistente != null)
            {
                _carrinho.Remove(produtoExistente);
            }
        }

        public void LimparCarrinho()
        {
            ProdutosCarregados = false;
        }
    }
}
