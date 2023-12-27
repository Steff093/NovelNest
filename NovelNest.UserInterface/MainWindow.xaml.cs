﻿using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
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

        private void loginWindowTest_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
        }

        private void closeApplication_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Möchten Sie diese Anwendung wirklich schließen?",
                "NovelNest schließen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    return;
            }
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