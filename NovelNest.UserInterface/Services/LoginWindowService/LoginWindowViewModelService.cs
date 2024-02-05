using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UserInterface.ViewModels.LoginViewModel;
using NovelNest.UserInterface.ViewModels.RegistrationViewModel;

namespace NovelNest.UserInterface.Services.LoginWindowService
{
    public class LoginWindowViewModelService
    {
        private LoginViewModels _loginViewModel;

        public LoginViewModels GetLoginWindowView(
            IDialogProvider dialogProvider,
            IRegistrationFeatures registrationFeatures,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
        {
            if (_loginViewModel == null)
                _loginViewModel = new LoginViewModels(
                    dialogProvider,
                    registrationFeatures,
                    passwordHasher,
                    navigationService);
            return _loginViewModel;
        }
    }


    /*
     *             IDialogProvider dialogProvider,
            IRegistrationFeatures registrationFeatures,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
    */
}
