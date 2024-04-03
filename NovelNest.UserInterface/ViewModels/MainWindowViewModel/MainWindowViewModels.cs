using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Features.FolderFeature.FolderAddFeature;
using NovelNest.ApplicationLogic.Features.FolderFeature.FolderDeleteFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookAddRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookDeleteRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookUpdateRepository;
using NovelNest.Infrastructure.Repositories.FolderRepositories.FolderAddRepositories;
using NovelNest.Infrastructure.Repositories.FolderRepositories.FolderDeleteRepositories;
using NovelNest.UI.Domain.Entities.LoginEntities;
using NovelNest.UserInterface.Services.BookManagementService;
using NovelNest.UserInterface.UserControlView;
using NovelNest.UserInterface.ViewModels.BookManagementViewModel;
using NovelNest.UserInterface.ViewModels.FolderManagementViewModel;
using NovelNest.UserInterface.ViewModels.LoginViewModel;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.UpdateView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NovelNest.UserInterface.ViewModels.MainWindowViewModel
{
    public class MainWindowViewModels : BaseViewModel
    {
        private readonly IDialogProvider _dialogProvider;
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public MainWindowViewModels()
        {
        }

        public MainWindowViewModels(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            BookButton();
        }

        #region Comandas

        public ICommand BookManagement => new RelayCommand(BookManagementButton);
        public ICommand FolderManagement => new RelayCommand(FolderManagementButton);
        public ICommand MouseDrag => new RelayCommand(MouseDownDrag);
        public ICommand CloseApplicationCommand => new RelayCommand(CloseApplication);

        #endregion

        private void BookButton()
        {
            /*
                * Richtigen Konstruktor nutzen!
                * Hier DataContext setzen, damit alle Daten angezeigt werden!
             */

            BookManagementViewModels bookManagementViewModel = new BookManagementViewModels(
                new AddBookFeature(new BookAddRepository(new NovelNestDataContext())),
                new UpdateBookFeature(new BookUpdateRepository(new NovelNestDataContext())),
                new DialogProvider(),
                new DeleteBookFeature(new BookDeleteRepository(new NovelNestDataContext())));

            CurrentView = new BookManagementView()
            {
                DataContext = bookManagementViewModel
            };
        }

        private void BookManagementButton()
        {
            BookButton();
        }

        private void FolderManagementButton()
        {
            FolderButton();
        }

        private void FolderButton()
        {
            var folderViewModel = new FolderManagementViewModels(
                new DialogProvider(),
                new AddFolderFeature(new FolderAddRepository(new NovelNestDataContext())),
                new DeleteFolderFeatures(new FolderDeleteRepository(new NovelNestDataContext())));

            CurrentView = new FolderManagementView()
            {
                DataContext = folderViewModel
            };
        }

        private void MouseDownDrag()
        {
            var mainWindow = Application.Current.MainWindow as Window;

            if (mainWindow is not null && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                mainWindow.DragMove();
            }
        }

        #region Schließen der Anwendung

        /* Anwendung wird zurzeit nur über das Menü geschlossen
         * ToDo: Auf das X Der Anwendung erweitern
         * Allerdings kann man an das Closing .. "Element(?)" vom MainWindow 
         * keinen Command binden. 
        */

        private void CloseApplication()
        {
            bool result = _dialogProvider.ShowConfirmation("NovelNest schließen", "Möchten Sie die Anwendung wirklich schließen?");

            if (result)
            {
                // Prüfen, ob Anwendung schon geschlossen ist oder nicht 

                if (Application.Current is not null)
                    Application.Current.Shutdown();
            }
            else
                return;
        }
        #endregion
    }
}

