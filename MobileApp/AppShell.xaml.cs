using MobileApp.Pages;
using MobileApp.Pages.Clientes;
using MobileApp.Pages.Home;
using MobileApp.Pages.Vendas;

namespace MobileApp;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ProdutosPage), typeof(ProdutosPage));
        Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
        Routing.RegisterRoute(nameof(ClientesPage), typeof(ClientesPage));
        Routing.RegisterRoute(nameof(HistoricoPage), typeof(HistoricoPage));
        Routing.RegisterRoute(nameof(PedidoPage), typeof(PedidoPage));
        Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
        Routing.RegisterRoute(nameof(CadastroClientePage), typeof(CadastroClientePage));
    }
}
