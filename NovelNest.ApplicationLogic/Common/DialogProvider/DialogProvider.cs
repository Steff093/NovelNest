using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using System.Windows;

namespace NovelNest.ApplicationLogic.Common.DialogProvider
{
    public class DialogProvider : IDialogProvider
    {
        public void ShowMessage(string noteMessage, string headMessageText)
        {
            MessageBox.Show(headMessageText, noteMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowConfirmation(string noteMessage, string headMessageText)
        {
            MessageBoxResult result = MessageBox.Show(headMessageText, noteMessage, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void ShowError(string message, string headMessage)
        {
            MessageBoxResult result = MessageBox.Show(headMessage, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
