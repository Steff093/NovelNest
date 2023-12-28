using NovelNest.Domain.Entities.BookEntities;
using NovelNest.UserInterface.MainWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovelNest.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
         
        }

        private void registrationTest_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView view = new();
            view.Show();
        }

        public void NavigateToRegistrationView()
        {
            RegistrationView view = new RegistrationView();
            view.ShowDialog();
        }

        public void NavigateToLoginView()
        {
            LoginView view = new LoginView();
            view.ShowDialog();
        }
    }
}