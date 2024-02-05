
using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.UserInterface.ViewModels.BookManagementViewModel;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;

namespace NovelNest.UserInterface.Services.MainWindowService
{
    public class MainWindowViewModelService
    {
        private MainWindowViewModels _mainWindowViewModel;

        public MainWindowViewModels GetMainViewModel()
        {
            if (_mainWindowViewModel is null)
                _mainWindowViewModel = new MainWindowViewModels(
                    new DialogProvider());

            return _mainWindowViewModel;
        }
    }
}
