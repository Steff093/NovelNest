using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces.IDialogProvider
{
    public interface IDialogProvider
    {
        void ShowMessage(string notemessage, string headMessage);
        bool ShowConfirmation(string noteMessage, string headMessageText);
        void ShowError(string noteMessage, string headMessage);
    }
}
