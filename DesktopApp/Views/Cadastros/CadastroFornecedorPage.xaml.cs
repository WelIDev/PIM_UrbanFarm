using System.Windows;
using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Views.Cadastros
{
    public partial class CadastroFornecedorPage
    {
        private readonly ApiService _apiService;

        public CadastroFornecedorPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void CadastrarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var fornecedor = new FornecedorModel
            {
                Nome = NomeFornecedorTextBox.Text,
                Email = EmailTextBox.Text,
                Cnpj = CnpjTextBox.Text,
                InscricaoEstadual = InscricaoEstadualTextBox.Text,
                Telefone = TelefoneTextBox.Text,
                Endereco = new EnderecoModel
                {
                    Rua = RuaTextBox.Text,
                    Numero = int.Parse(NumeroTextBox.Text),
                    Bairro = BairroTextBox.Text,
                    Cidade = CidadeTextBox.Text,
                    Estado = EstadoTextBox.Text,
                    Cep = CepTextBox.Text
                }
            };

            bool success = await _apiService.CadastrarFornecedorAsync(fornecedor);
            if (success)
            {
                MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Falha ao cadastrar fornecedor. Tente novamente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
