using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Common.PasswordHash;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Features.RegistrationFeatures;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.AddBookInterfaceInfrastructure;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.DeleteBookInterfaceInfrastructure;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.UpdateeBookInterfaceInfrastructure;
using NovelNest.Infrastructure.Interfaces.IRegistrationInterfaceInfrastructure;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookAddRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookDeleteRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookUpdateRepository;
using NovelNest.Infrastructure.Repositories.RegistrationRepositories;
using NovelNest.UserInterface.Services.NavigationService;
using NovelNest.UserInterface.ViewModels.LoginViewModel;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
using NovelNest.UserInterface.ViewModels.RegistrationViewModel;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
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

            var loginWindow = new LoginView()
            {
                DataContext = _serviceProvider.GetRequiredService<LoginViewModels>()
            };
            loginWindow.ShowDialog();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<NovelNestDataContext>();
            serviceCollection.AddSingleton<IBookAddRepository, BookAddRepository>();
            serviceCollection.AddSingleton<IBookUpdateRepository, BookUpdateRepository>();
            serviceCollection.AddSingleton<IBookDeleteRepository, BookDeleteRepository>();
            serviceCollection.AddSingleton<IRegistrationRepository, RegistrationRepository>();
            serviceCollection.AddSingleton<IDialogProvider, DialogProvider>();
            serviceCollection.AddSingleton<IAddBookFeature, AddBookFeature>();
            serviceCollection.AddSingleton<IUpdateBookFeature, UpdateBookFeature>();
            serviceCollection.AddSingleton<IDeleteBookFeature, DeleteBookFeature>();
            serviceCollection.AddSingleton<IRegistrationFeatures, RegistrationUserFeature>();
            serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<BookEntity>();
            serviceCollection.AddSingleton<LoginViewModels>();
            serviceCollection.AddSingleton<MainWindowViewModels>();
            serviceCollection.AddSingleton<UpdateWindowViewModels>();
            serviceCollection.AddSingleton<RegistrationViewModels>();
        }
    }

}
