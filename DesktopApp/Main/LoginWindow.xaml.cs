using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace DesktopApp.Main;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private static readonly HttpClient _client = new HttpClient();
    
    public LoginWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
    }

    private async void Login_OnClick(object sender, RoutedEventArgs e)
    {
        

        var loginData = new
        {
            email = Email.Text,
            senha = Senha.Password
        };

        var json = JsonConvert.SerializeObject(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _client.PostAsync
                ("https://localhost:7124/api/Autenticacao/Login", content);
            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadAsStringAsync();

            var menu = new MenuWindow();
            menu.Show();
            
            MessageBox.Show("Login realizado com sucesso!");
            this.Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Erro ao fazer login: {exception.Message}");
        }
    }
}
