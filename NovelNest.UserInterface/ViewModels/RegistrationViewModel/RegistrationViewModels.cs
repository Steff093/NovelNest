using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Common.PasswordHash;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.RegistrationViewModel
{
    public class RegistrationViewModels : BaseViewModel
    {
        private readonly IRegistrationFeatures _registrationFeatures;
        private readonly IDialogProvider _dialogProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly INavigationService _navigateService;

        public ICommand RegisterButtonCommand => new RelayCommand(RegisterCommand);
        public ICommand LoginWindow => new RelayCommand(LoginNavigateWindow);

        public RegistrationViewModels()
        {

        }

        public RegistrationViewModels(
            IRegistrationFeatures registrationFeatures,
            IDialogProvider dialogProvider,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
        {
            _registrationFeatures = registrationFeatures;
            _dialogProvider = dialogProvider;
            _passwordHasher = passwordHasher;
            _navigateService = navigationService;
        }

        private string _registrationName;
        private string _registrationPassword;
        private string _registrationEmail;

        public string RegistrationName
        {
            get => _registrationName;
            set
            {
                _registrationName = value;
                OnPropertyChanged(nameof(RegistrationName));
            }
        }

        public string RegistrationPassword
        {
            get => _registrationPassword;
            set
            {
                _registrationPassword = value;
                OnPropertyChanged(nameof(RegistrationPassword));
            }
        }

        public string RegistrationEmail
        {
            get => _registrationEmail;
            set
            {
                _registrationEmail = value;
                OnPropertyChanged(nameof(RegistrationEmail));
            }
        }

        public void RegisterCommand()
        {
            if (string.IsNullOrEmpty(RegistrationName) || string.IsNullOrEmpty(RegistrationPassword) || string.IsNullOrEmpty(RegistrationEmail))
            {
                _dialogProvider.ShowError("Fehler", "Bitte tragen Sie einen Namen, Password und eine Email ein!");
                return;
            }

            if (_passwordHasher == null)
            {
                return;
            }
            var salt = _passwordHasher.GenerateRandomSalt();
            var hashedPassword = _passwordHasher.PBKDF2Hash(RegistrationPassword, salt);

            if (!string.IsNullOrEmpty(RegistrationName) || !string.IsNullOrEmpty(RegistrationPassword) || !string.IsNullOrEmpty(RegistrationEmail))
            {
                UserEntity entity = new UserEntity()
                {
                    UserName = RegistrationName,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    EmailAddress = RegistrationEmail
                };

                _registrationFeatures.AddUserToTable(entity);

                _dialogProvider.ShowMessage("Erfolgreich", "Benutzer erfolgreich hinzugefügt!");
            }
        }

        private void LoginNavigateWindow()
        {
            //_navigateService.NavigateToLoginWindow();
            App.NavigateToLogin();
        }
    }
}
