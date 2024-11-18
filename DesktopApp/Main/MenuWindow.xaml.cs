using System.Windows;
using DesktopApp.Dashboards;

namespace DesktopApp.Main;

public partial class MenuWindow : Window
{
    public MenuWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
    }

    private void Vendas_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new VendasPage());
    }

    private void Produtos_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new ProdutosPage());
    }

    private void EntradasSaidas_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new EntradasSaidasPage());
    }
}

