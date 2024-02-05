using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Common.PasswordHash;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Features.RegistrationFeatures;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookAddRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookDeleteRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookUpdateRepository;
using NovelNest.Infrastructure.Repositories.RegistrationRepositories;
using NovelNest.UserInterface.Services.LoginWindowService;
using NovelNest.UserInterface.Services.MainWindowService;
using NovelNest.UserInterface.Services.RegistrationWindowService;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
using System.Windows;
using System.Windows.Navigation;


namespace NovelNest.UserInterface.Services.NavigationService
{
    public class NavigationService : INavigationService
    {

        /*
         * Versuche, die Methoden in der App.xaml.cs reinzuschreiben, um dann von der App.xaml.cs aus auf die Methoden zuzugreifen
         * Somit wäre das Problem mit der UI in einer Klasse theoretisch auch gelöst. 
         * Und wenn man von Login auf Registration wechselt, soll der Login oder die Registration geschlossen werden.
        */

        public void NavigateToMainWindow()
        {
            var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault(x => x.IsActive);
            var mainWindowViewModelService = new MainWindowViewModelService();
            var mainWindowViewModel = mainWindowViewModelService.GetMainViewModel();

            if (loginWindow is not null)
            {
                var mainWindow = new MainWindow()
                {
                    DataContext = mainWindowViewModel,
                };
                loginWindow.Close();
                mainWindow.ShowDialog();
                mainWindow.Activate();
            }
        }

        public void NavigateToRegistrationWindow()
        {
            //var registtationViewModelService = new LoginWindowViewModelService();
            //var registrationViewModel = registtationViewModelService.GetRegistrationViewModels(
            //    new RegistrationUserFeature(new RegistrationRepository(new NovelNestDataContext())),
            //    new DialogProvider(),
            //    new PasswordHasher(),
            //    new NavigationService());

            //if (registrationViewModel is not null)
            //{
            //    var registration = new RegistrationView()
            //    {
            //        DataContext = registrationViewModel,
            //    };
            //    registration.ShowDialog();
            //    registration.Activate();
            //}
        }

        public void NavigateToLoginWindow()
        {
            //var loginViewModelService = new RegistrationWindowViewModelService();
            //var loginViewModel = loginViewModelService.GetRegistrationView(
            //    new DialogProvider(),
            //    new RegistrationUserFeature(new RegistrationRepository(new NovelNestDataContext())),
            //    new PasswordHasher(),
            //    new NavigationService());

            //if (loginViewModel is not null)
            //{
            //    var login = new LoginView()
            //    {
            //        DataContext = loginViewModel,
            //    };
            //    login.ShowDialog();
            //    login.Activate();
            //}
        }
    }
}
