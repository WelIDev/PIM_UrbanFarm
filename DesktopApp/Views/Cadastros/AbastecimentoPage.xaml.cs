using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Views.Cadastros
{
    public partial class AbastecimentoPage : Page
    {
        private readonly ApiService _apiService;
        public ObservableCollection<ItemAbastecimentoModel> Produtos { get; set; }
        public ObservableCollection<ProdutoModel> ProdutosList { get; set; }
        public ObservableCollection<FornecedorModel> Fornecedores { get; set; }

        public AbastecimentoPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Produtos = new ObservableCollection<ItemAbastecimentoModel>();
            ProdutosList = new ObservableCollection<ProdutoModel>();
            Fornecedores = new ObservableCollection<FornecedorModel>();
            DataContext = this; // Configura o DataContext para vinculação de dados
            LoadData();
        }

        private async void LoadData()
        {
            // Carregar Produtos
            var produtos = await _apiService.GetProdutosAsync();
            foreach (var produto in produtos)
            {
                ProdutosList.Add(produto);
            }

            // Carregar Fornecedores
            var fornecedores = await _apiService.GetFornecedoresAsync();
            foreach (var fornecedor in fornecedores)
            {
                Fornecedores.Add(fornecedor);
            }
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            Produtos.Add(new ItemAbastecimentoModel());
        }

        private void RemoverProduto_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ItemAbastecimentoModel produto)
            {
                Produtos.Remove(produto);
            }
        }

        private async void CadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            // Navegar para a página de cadastro de produto
            NavigationService.Navigate(new CadastroProdutoPage());
        }

        private async void CadastrarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            // Navegar para a página de cadastro de fornecedor
            NavigationService.Navigate(new CadastroFornecedorPage());
        }

        private async void CadastrarAbastecimento_Click(object sender, RoutedEventArgs e)
        {
            if (FornecedorComboBox.SelectedItem is FornecedorModel fornecedor)
            {
                var abastecimentoDto = new AbastecimentoModel
                {
                    FornecedorId = fornecedor.Id,
                    UsuarioId = 1, // Substitua pelo ID do usuário logado
                    Observacoes = ObservacoesTextBox.Text,
                    Produtos = new List<ItemAbastecimentoModel>(Produtos)
                };

                bool success = await _apiService.CadastrarAbastecimentoAsync(abastecimentoDto);
                if (success)
                {
                    MessageBox.Show("Abastecimento cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao cadastrar abastecimento. Tente novamente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
