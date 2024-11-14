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
        throw new NotImplementedException();
    }

    private void Fornecimento_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}

