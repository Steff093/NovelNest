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

namespace NovelNest.UserInterface.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public string ConnectionString { get; set; }

        public SettingsView()
        {
            InitializeComponent();
        }

        private void connectionButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionString = txtConnectionString.Text;
            DialogResult = true;
        }
    }
}
