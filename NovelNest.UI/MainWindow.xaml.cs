﻿using NovelNest.UI.Views.Login;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovelNest.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void loginWindowTest_Click(object sender, RoutedEventArgs e)
        {
            LoginView log = new LoginView();

            log.Show();
        }
    }
}
