using Microsoft.Extensions.DependencyInjection;
using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using NovelNest.Infrastructure.Repositories.BookAddRepository;
using NovelNest.Infrastructure.Repositories.BookDeleteRepository;
using NovelNest.Infrastructure.Repositories.BookUpdateRepository;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
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
            serviceCollection.AddScoped<IBookUpdateRepository<BookEntity>, BookUpdateRepository>();
            serviceCollection.AddScoped<IBookDeleteRepository, BookDeleteRepository>();
            serviceCollection.AddScoped<IAddBookFeature<BookEntity>, AddBookFeature>();
            serviceCollection.AddScoped<IUpdateBookFeature<BookEntity>, UpdateBookFeature>();
            serviceCollection.AddScoped<IDeleteBooKFeature, DeleteBookFeature>();
            serviceCollection.AddScoped<BookEntity>();
            serviceCollection.AddScoped<IDialogProvider, DialogProvider>();
            serviceCollection.AddScoped<MainWindowViewModels>();
            serviceCollection.AddScoped<UpdateWindowViewModels>();
        }
    }

}
