using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.LoginInterface;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure;
using NovelNest.UserInterface.ViewModels.LoginViewModel;
using NovelNest.UserInterface.ViewModels.RegistrationViewModel;

namespace NovelNest.UserInterface.Services.LoginWindowService
{
    public class LoginWindowViewModelService
    {
        private LoginViewModels _loginViewModel;

        public LoginViewModels GetLoginWindowView(
            IDialogProvider dialogProvider,
            ILoginFeatures loginInterface,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
        {
            if (_loginViewModel == null)
                _loginViewModel = new LoginViewModels(
                    dialogProvider,
                    loginInterface,
                    passwordHasher,
                    navigationService);
            return _loginViewModel;
        }
    }
}
