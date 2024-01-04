using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NovelNest.ApplicationLogic.Common.DialogProvider
{
    public class DialogProvider : IDialogProvider
    {
        public void ShowMessage(string noteMessage, string headMessageText)
        {
            MessageBox.Show(noteMessage, headMessageText, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowConfirmation(string noteMessage, string headMessageText)
        {
            MessageBoxResult result = MessageBox.Show(noteMessage, headMessageText, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void ShowError(string message, string headMessage)
        {
            MessageBoxResult result = MessageBox.Show(message, headMessage, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
