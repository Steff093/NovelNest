using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface;

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

        public IRelayCommand LoginCommand { get; private set; }
        public IRelayCommand RegisterCommand { get; private set; }

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            //LoginCommand = new RelayCommand(Login, CanLogin);
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
