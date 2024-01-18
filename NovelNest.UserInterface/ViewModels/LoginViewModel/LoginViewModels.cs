using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.LoginViewModel
{
    public class LoginViewModels : INotifyPropertyChanged
    {
        public ICommand LoginConnect => new RelayCommand(LoginConnectCommand);
        public ICommand CloseWindow => new RelayCommand(LoginCloseRequest);

        public event EventHandler<EventArgs> CloseLogin;

        private readonly IDialogProvider _dialogProvider;
        private readonly IRegistrationFeatures _registrationFeatures;
        private readonly IPasswordHasher _passwordHasher;
        private readonly INavigationService _navigationService;

        public LoginViewModels()
        {

        }

        public LoginViewModels(
            IDialogProvider dialogProvider,
            IRegistrationFeatures registrationFeatures,
            IPasswordHasher passwordHasher,
            INavigationService navigationService)
        {
            _dialogProvider = dialogProvider;
            _registrationFeatures = registrationFeatures;
            _passwordHasher = passwordHasher;
            _navigationService = navigationService;
        }

        private string _userName;
        private string _password;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private void LoginCloseRequest()
        {
            CloseLogin?.Invoke(this, EventArgs.Empty);
        }

        private async void LoginConnectCommand()
        {
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
            {
                _dialogProvider.ShowError("Bitte geben Sie einen Benutzernamen und Password ein!", "Anmeldefehler");
                return;
            }

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {

                var userFromTable = await _registrationFeatures.GetUserByUsername(UserName);

                if (userFromTable is null)
                {
                    return;
                }
                if (userFromTable is not null)
                {
                    var saltFromDatabase = userFromTable.PasswordSalt;
                    var hashedPassword = userFromTable.PasswordHash;

                    var comparisonPassword = _passwordHasher.PBKDF2Hash(Password, saltFromDatabase);

                    if (comparisonPassword.SequenceEqual(hashedPassword))
                    {
                        _dialogProvider.ShowMessage("Login Erfolgreich", "Erfolg!");
                        _navigationService.NavigateToMainWindow();
                        LoginCloseRequest();

                    }
                    else
                    {
                        _dialogProvider.ShowError("Benutzername oder Password falsch!", "Fehler");
                        UserName = string.Empty; Password = string.Empty;
                        return;
                    }
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
