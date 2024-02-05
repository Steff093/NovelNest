using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.NavigationService;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.LoginViewModel
{
    public class LoginViewModels : INotifyPropertyChanged
    {
        public ICommand LoginConnect => new RelayCommand(LoginConnectCommand);
        public ICommand RegisterWindow => new RelayCommand(NavigateToRegistrationView);
        public ICommand CloseCommand => new RelayCommand(ClosingLogin);
        public ICommand MouseDrag => new RelayCommand(MouseDownDrag);

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

        private async void LoginConnectCommand()
        {
            //if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
            //{
            //    _dialogProvider.ShowError("Anmeldefehler", "Bitte geben Sie einen Benutzernamen und Password ein!");
            //    return;
            //}

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                var userFromTable = await _registrationFeatures.GetUserByUsername(UserName);

                if (userFromTable is not null)
                {
                    byte[]? saltFromDatabase = userFromTable.PasswordSalt;
                    byte[]? hashedPassword = userFromTable.PasswordHash;

                    var comparisonPassword = _passwordHasher.PBKDF2Hash(Password, saltFromDatabase);

                    if (comparisonPassword.SequenceEqual(hashedPassword))
                    {
                        _dialogProvider.ShowMessage("Erfolg!", "Login Erfolgreich. Willkommen " + UserName);


                        // Muss Überlegen was besser ist - eigene Klasse für die Verwaltung oder in App.xaml.cs
                        // Momentan läuft es über die App.xaml.cs 

                        //_navigationService.NavigateToMainWindow();

                        App.NavigateToMainWindow();
                    }
                    else
                    { 
                        _dialogProvider.ShowError("Fehler", "Benutzername oder Password falsch!");
                        UserName = string.Empty; Password = string.Empty;
                        return;
                    }
                }
                else
                {
                    _dialogProvider.ShowError("Fehler", "Benutzer wurde nicht gefunden");
                    UserName = string.Empty; Password = string.Empty;
                    return;
                }
            }
            else
            {
                _dialogProvider.ShowError("Anmeldefehler", "Bitte geben Sie Benutzernamen und Password ein!");
                return;
            }
        }

        private void NavigateToRegistrationView()
        {
            App.NavigateToRegister();
        }

        private void ClosingLogin()
        {
            _dialogProvider.ShowConfirmation("NovelNest schließen", "Möchten Sie die Anwendung wirklich schließen?");
        }

        private void MouseDownDrag()
        {
            var loginWindow = Application.Current.MainWindow as Window;

            if(loginWindow is not null && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                loginWindow.DragMove();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
