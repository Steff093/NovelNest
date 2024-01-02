using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface;
using NovelNest.ApplicationLogic.Common.Interfaces.MainWindowInterface;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.ApplicationLogic.Services.BookService;
using NovelNest.ApplicationLogic.Services.LoginService;
using NovelNest.ApplicationLogic.Services.MainWindowService;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using NovelNest.Infrastructure.Repositories.BookAddRepository;
using NovelNest.UI.Views.LoginView;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views;
using System.Configuration;
using System.Windows;

namespace NovelNest.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {

            var serviceCollection = new ServiceCollection();

            base.OnStartup(e);

            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = new MainWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<MainWindowViewModels>()
            };
            mainWindow.ShowDialog();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<NovelNestDataContext>();
            serviceCollection.AddScoped<IBookAddRepository<BookEntity>, BookAddRepository>();
            serviceCollection.AddScoped<IAddBookFeature<BookEntity>, AddBookFeature>();
            serviceCollection.AddScoped<BookEntity>();
            serviceCollection.AddScoped<MainWindowViewModels>();
            serviceCollection.AddScoped<UpdateWindowViewModels>();
        }
    }

}
