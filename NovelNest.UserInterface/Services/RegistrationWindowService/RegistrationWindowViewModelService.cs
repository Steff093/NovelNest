using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UserInterface.ViewModels.LoginViewModel;
using NovelNest.UserInterface.ViewModels.RegistrationViewModel;

namespace NovelNest.UserInterface.Services.RegistrationWindowService
{
    public class RegistrationWindowViewModelService
    {
        private RegistrationViewModels _registrationViewModels;

        public RegistrationViewModels GetRegistrationView(
            IRegistrationFeatures registrationFeatures,
            IDialogProvider dialogProvider,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)

        {
            if (_registrationViewModels == null)
                _registrationViewModels = new RegistrationViewModels(
                    registrationFeatures,
                    dialogProvider,
                    passwordHasher,
                    navigationService);
            return _registrationViewModels;
        }
    }
}
