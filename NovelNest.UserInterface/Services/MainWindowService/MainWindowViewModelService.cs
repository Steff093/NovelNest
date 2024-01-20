
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;

namespace NovelNest.UserInterface.Services.MainWindowService
{
    public class MainWindowViewModelService
    {
        private MainWindowViewModels _mainWindowViewModel;

        public MainWindowViewModels GetMainViewModel(
            IAddBookFeature addBookFeature,
            IUpdateBookFeature updateBookFeature,
            IDialogProvider dialogProvider,
            IDeleteBookFeature deleteBooKFeature)
        {
            if(_mainWindowViewModel is null)
                _mainWindowViewModel = new MainWindowViewModels(
                    addBookFeature,
                    updateBookFeature,
                    dialogProvider,
                    deleteBooKFeature);

            return _mainWindowViewModel;
        }
    }
}
