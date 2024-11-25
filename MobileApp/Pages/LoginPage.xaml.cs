using MobileApp.Services;

namespace MobileApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly SessionService _sessionService;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            _sessionService = SessionService.Instance;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = UsernameEntry.Text;
            var senha = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                await DisplayAlert("Erro", "Por favor, insira seu nome de usuário e senha", "OK");
                return;
            }

            var usuario = await _apiService.LoginAsync(email, senha);

            if (usuario != null)
            {
                _sessionService.SetUsuario(usuario);
                await DisplayAlert($"Sucesso", $"Login realizado com sucesso!", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Erro", "Nome de usuário ou senha inválidos", "OK");
            }
        }
    }
}
