using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UserInterface.ViewModels.LoginViewModel;

namespace NovelNest.UserInterface.Services.RegistrationWindowService
{
    public class RegistrationWindowViewModelService
    {
        private LoginViewModels _loginViewModels;

        public LoginViewModels GetLoginView(
            IDialogProvider dialogProvider,
            IRegistrationFeatures registrationFeatures,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)

        {
            if (_loginViewModels == null)
                _loginViewModels = new LoginViewModels(
                    dialogProvider,
                    registrationFeatures,
                    passwordHasher,
                    navigationService);
            return _loginViewModels;
        }
    }
}
