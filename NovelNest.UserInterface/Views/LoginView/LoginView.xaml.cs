using System.Windows;

namespace NovelNest.UserInterface.Views.LoginView
{
    /// <summary>
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void lnkRegister_Click(object sender, RoutedEventArgs e)
        {
            Close();

            // ToDo:  Register Verlinkung neu schreiben

            //var mainWindowViewModel = new MainWindowViewModels();
            //mainWindowViewModel.RegistrationApplicationCommand();
        }
    }
}
