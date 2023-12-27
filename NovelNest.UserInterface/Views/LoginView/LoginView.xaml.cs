﻿using System;
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

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToRegistrationView();
        }
    }
}
