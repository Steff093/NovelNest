using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UserInterface.ViewModels.RegistrationViewModel;

namespace NovelNest.UserInterface.Services.LoginWindowService
{
    public class LoginWindowViewModelService
    {
        private RegistrationViewModels _registrationViewModel;

        public RegistrationViewModels GetRegistrationViewModels(
            IRegistrationFeatures registrationFeatures,
            IDialogProvider dialogProvider,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
        {
            if (_registrationViewModel == null)
                _registrationViewModel = new RegistrationViewModels(
                    registrationFeatures,
                    dialogProvider,
                    passwordHasher,
                    navigationService);
            return _registrationViewModel;
        }
    }
}
