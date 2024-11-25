using MobileApp.Pages;
using MobileApp.Services;

namespace MobileApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            ConfigureServices();

            UserAppTheme = AppTheme.Light;
            MainPage = new AppShell(); // Defina a página principal aqui
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<CartService>();  // Adiciona CartService como singleton

            services.AddSingleton<SessionService>();
            
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
