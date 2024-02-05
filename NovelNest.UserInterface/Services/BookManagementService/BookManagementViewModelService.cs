using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.UserInterface.ViewModels.BookManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.UserInterface.Services.BookManagementService
{
    public class BookManagementViewModelService
    {
        private BookManagementViewModels _bookManagementViewModel;

        public BookManagementViewModels GetBookManagement(
            IAddBookFeature addBookFeature,
            IUpdateBookFeature updateBookFeature,
            IDialogProvider dialogProvider,
            IDeleteBookFeature deleteBooKFeature)
        {
            if (_bookManagementViewModel is null)
                _bookManagementViewModel = new BookManagementViewModels(
                    addBookFeature,
                    updateBookFeature,
                    dialogProvider,
                    deleteBooKFeature);

            return _bookManagementViewModel;
        }
    }
}
