using System.Windows;
using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Views.Cadastros
{
    public partial class CadastroProdutoPage
    {
        private readonly ApiService _apiService;

        public CadastroProdutoPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void CadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            var produto = new ProdutoModel
            {
                Nome = NomeProdutoTextBox.Text,
                Preco = double.Parse(PrecoTextBox.Text),
                Estoque = int.Parse(EstoqueTextBox.Text),
                Descricao = DescricaoTextBox.Text
            };

            bool success = await _apiService.CadastrarProdutoAsync(produto);
            if (success)
            {
                MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.GoBack();
            }
            else
            {
                MessageBox.Show("Falha ao cadastrar produto. Tente novamente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
