using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.Pages.Clientes
{
    public partial class CadastroClientePage
    {
        private readonly ApiService _apiService;

        public CadastroClientePage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            var cliente = new ClienteModel
            {
                Nome = NomeEntry.Text,
                Email = EmailEntry.Text,
                CpfCnpj = CpfCnpjEntry.Text,
                Telefone = TelefoneEntry.Text,
                DataNascimento = DataNascimentoPicker.Date,
                Endereco = new EnderecoModel
                {
                    Rua = RuaEntry.Text,
                    Numero = int.Parse(NumeroEntry.Text),
                    Bairro = BairroEntry.Text,
                    Cidade = CidadeEntry.Text,
                    Estado = EstadoEntry.Text,
                    Cep = CepEntry.Text
                }
            };
            
            bool success = await _apiService.CadastrarClienteAsync(cliente);
            if (success)
            {
                await DisplayAlert("Sucesso", "Cliente cadastrado com sucesso!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao cadastrar cliente. Tente novamente.", "OK");
            }
        }

        private async void OnCepSearchClicked(object? sender, EventArgs e)
        {
            var cep = CepEntry.Text;

            if (string.IsNullOrEmpty(cep))
            {
                await DisplayAlert("Erro", "Por favor, insira um CEP válido", "OK");
                return;
            }

            var endereco = await _apiService.BuscarEnderecoPorCepAsync(cep);
            if (endereco != null)
            {
                RuaEntry.Text = endereco.Rua;
                NumeroEntry.Text = endereco.Numero.ToString();
                BairroEntry.Text = endereco.Bairro;
                CidadeEntry.Text = endereco.Cidade;
                EstadoEntry.Text = endereco.Estado;
                CepEntry.Text = endereco.Cep;
            }
            else
            {
                await DisplayAlert("Erro", "CEP não encontrado", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//HomePage");
            return true;
        }
    }
}
