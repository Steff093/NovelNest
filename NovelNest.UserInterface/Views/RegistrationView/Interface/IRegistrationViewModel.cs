using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovelNest.UserInterface.Views.RegistrationView.Interface
{
    public interface IRegistrationViewModel
    {
        string Name { get; set; }
        string Password { get; set; } 
        string Email { get; set; }

        ICommand RegisterCommand { get; } 
    }
}
