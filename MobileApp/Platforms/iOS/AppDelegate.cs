using Foundation;
using UIKit;

namespace MobileApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        // Configura a cor da barra de status
        UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
        UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(23, 32, 49); // Cor #172031 em RGB

        // Altere a cor dos ícones e texto na barra de navegação
        UINavigationBar.Appearance.TintColor = UIColor.White;

        return base.FinishedLaunching(app, options);
    }
}
