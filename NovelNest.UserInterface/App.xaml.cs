using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface;
using NovelNest.ApplicationLogic.Services.LoginService;
using NovelNest.UI.Views.LoginView;
using System.Windows;

namespace NovelNest.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = new MainWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<LoginViewModel>()
            };

            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoginService, LoginService>();

            serviceCollection.AddTransient<LoginViewModel>();
        }
    }

}
