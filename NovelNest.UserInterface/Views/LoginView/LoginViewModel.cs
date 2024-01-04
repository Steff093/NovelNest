using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Navigation;
using NovelNest.ApplicationLogic.Interfaces.LoginInterface;

namespace NovelNest.UI.Views.LoginView
{
    public class LoginViewModel : ObservableObject
    { 
        private string _username;
        private string _password;

        private readonly ILoginService _loginService;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            LoginCommand = new RelayCommand(LoginAsync, CanLogin);
        }

        private async void LoginAsync()
        {

        }

        private bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
