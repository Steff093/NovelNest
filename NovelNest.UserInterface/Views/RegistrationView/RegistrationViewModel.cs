using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.UserInterface.Views.RegistrationView.Interface;
using System.ComponentModel;
using System.Windows.Input;

namespace NovelNest.UserInterface.Views.RegistrationView
{
    public class RegistrationViewModel : INotifyPropertyChanged, IRegistrationViewModel
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationViewModel(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        private string _name;
        private string _password;
        private string _email;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name 
        { 
            get => _name; 
            set => _name = value;

        }
        public string Password 
        { 
            get => _password; 
            set => _password = value; 
        }
        public string Email 
        { 
            get => _email; 
            set => _email = value;
        }

        public ICommand RegisterCommand => new RelayCommand(Register);

        public void Register()
        {

        }
    }
}
