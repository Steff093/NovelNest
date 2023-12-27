using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface;
using NovelNest.ApplicationLogic.Services.LoginService;
using NovelNest.Infrastructure.Database;
using NovelNest.UI.Views.LoginView;
using NovelNest.UserInterface.Views;
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

            var connectedWindow = new SettingsView();

            if (connectedWindow.ShowDialog() == true)
            {
                var connectedString = connectedWindow.ConnectionString;

                var dbContext = new DbContextOptionsBuilder<NovelNestDataContext>()
                    .UseSqlServer(connectedString)
                    .Options;

                using (var dbConnect = new NovelNestDataContext(dbContext))
                {
                    try
                    {
                        dbConnect.Database.Migrate();
                        MessageBox.Show("Verbindung erfolgreich!");
                    }
                    catch
                    {
                        MessageBox.Show("Fehler!");
                    }
                }
            }
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoginService, LoginService>();

            serviceCollection.AddTransient<LoginViewModel>();
        }
    }

}
