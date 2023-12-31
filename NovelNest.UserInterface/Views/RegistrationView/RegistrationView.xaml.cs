using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NovelNest.UserInterface.Views.RegistrationView
{
    /// <summary>
    /// Interaktionslogik für RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void lnkLogin_Click(object sender, RoutedEventArgs e)
        {
            Close();

            // ToDo: Login neu schreiben!

            //var mainWindow = (MainWindow)Application.Current.MainWindow;
            //mainWindow.NavigateToLoginView();
        }
    }
}
