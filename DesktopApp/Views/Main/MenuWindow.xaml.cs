using System.Windows;
using DesktopApp.Views.Cadastros;
using EntradasSaidasPage = DesktopApp.Views.Dashboards.EntradasSaidasPage;
using ProdutosPage = DesktopApp.Views.Dashboards.ProdutosPage;
using VendasPage = DesktopApp.Views.Dashboards.VendasPage;

namespace DesktopApp.Views.Main;

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

    private void Abastecimento_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new AbastecimentoPage());
    }
}

