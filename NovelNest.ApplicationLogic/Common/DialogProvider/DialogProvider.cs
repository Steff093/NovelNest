using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using Ookii.Dialogs.Wpf;
using System.Windows;

namespace NovelNest.ApplicationLogic.Common.DialogProvider
{
    public class DialogProvider : IDialogProvider
    {
        public void ShowMessage(string headMessageText, string noteMessage)
        {
            TaskDialog dialog = new();
            dialog.WindowTitle = headMessageText;
            dialog.Content = noteMessage;
            dialog.MainIcon = TaskDialogIcon.Information;
            TaskDialogButton button = new TaskDialogButton(ButtonType.Ok);
            dialog.Buttons.Add(button);
            dialog.ShowDialog();
        }

        public bool ShowConfirmation(string headMessageText, string noteMessage)
        {
            //MessageBoxResult result = MessageBox.Show(headMessageText, noteMessage, MessageBoxButton.YesNo, MessageBoxImage.Question);
            TaskDialog dialogConfirm = new();
            dialogConfirm.WindowTitle = headMessageText;
            dialogConfirm.Content = noteMessage;
            dialogConfirm.MainIcon = TaskDialogIcon.Information;
            TaskDialogButton buttonYes = new TaskDialogButton(ButtonType.Yes);
            TaskDialogButton buttonNo = new TaskDialogButton(ButtonType.No);
            dialogConfirm.Buttons.Add(buttonYes);
            dialogConfirm.Buttons.Add(buttonNo);

            TaskDialogButton clickedButton = dialogConfirm.ShowDialog();

            if (clickedButton == buttonYes)
            {
                if (Application.Current is not null)
                    Application.Current.Shutdown();
                return true;
            }
            else
                return false;
        }

        public void ShowError(string headMessageText, string noteMessage)
        {
            //MessageBoxResult result = MessageBox.Show(headMessage, message, MessageBoxButton.OK, MessageBoxImage.Error);

            TaskDialog dialogError = new();
            dialogError.WindowTitle = headMessageText;
            dialogError.Content = noteMessage;
            dialogError.MainIcon = TaskDialogIcon.Error;
            TaskDialogButton buttonOk = new TaskDialogButton(ButtonType.Ok);
            dialogError.Buttons.Add(buttonOk);
            dialogError.ShowDialog();

        }

        public bool ShowQuestionDelete(string headMessage, string noteMessage)
        {
            TaskDialog dialogDeleteBook = new();
            dialogDeleteBook.WindowTitle = headMessage;
            dialogDeleteBook.Content = noteMessage;
            dialogDeleteBook.MainIcon = TaskDialogIcon.Warning;
            TaskDialogButton buttonYes = new TaskDialogButton(ButtonType.Yes);
            TaskDialogButton buttonNo = new TaskDialogButton(ButtonType.No);
            dialogDeleteBook.Buttons.Add(buttonYes);
            dialogDeleteBook.Buttons.Add(buttonNo);

            TaskDialogButton clickedButton = dialogDeleteBook.ShowDialog();

            if (clickedButton == buttonYes)
                return true;
            else
                return false;
        }
    }
}
