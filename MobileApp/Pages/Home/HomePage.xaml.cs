using MobileApp.Services;

namespace MobileApp.Pages.Home;

public partial class HomePage : ContentPage
{
    private readonly SessionService _sessionService;

    public HomePage()
    {
        InitializeComponent();
        _sessionService = SessionService.Instance;
        var usuario = _sessionService.Usuario;
        if (usuario != null)
        {
                BemVindoLabel.Text = $"Bem Vindo, {usuario.Nome}!";
        }
    }

    private async void OnVerProdutosClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("ProdutosPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro",
                $"Ocorreu um erro ao tentar abrir a página de produtos: {ex.Message}", "OK");
        }
    }

    private async void OnVerClientesClicked(object? sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("ClientesPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro",
                $"Ocorreu um erro ao tentar abrir a página de clientes: {ex.Message}", "OK");
        }
    }
}
