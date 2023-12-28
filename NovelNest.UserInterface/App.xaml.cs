using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface;
using NovelNest.ApplicationLogic.Common.Interfaces.MainWindowInterface;
using NovelNest.ApplicationLogic.Features.BookFeatures;
using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.ApplicationLogic.Services.LoginService;
using NovelNest.ApplicationLogic.Services.MainWindowService;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using NovelNest.Infrastructure.Repositories;
using NovelNest.UI.Views.LoginView;
using NovelNest.UserInterface.MainWindowViewModel;
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
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMainWindowInterface, MainWindowService>();
            serviceCollection.AddScoped<IBookAddRepository<BookEntity>, BookAddRepository>();
            serviceCollection.AddScoped<IAddBookFeature<BookEntity>, AddBookFeature>();
            serviceCollection.AddScoped<MainWindowViewModels>();

        }
    }

}
