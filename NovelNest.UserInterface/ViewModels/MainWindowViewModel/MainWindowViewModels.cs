using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Common.DialogProvider;
using NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature;
using NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookAddRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookDeleteRepository;
using NovelNest.Infrastructure.Repositories.BookRepositories.BookUpdateRepository;
using NovelNest.UserInterface.Services.BookManagementService;
using NovelNest.UserInterface.UserControlView;
using NovelNest.UserInterface.ViewModels.BookManagementViewModel;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.UpdateView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.MainWindowViewModel
{
    public class MainWindowViewModels :INotifyPropertyChanged
    {

        private readonly IDialogProvider _dialogProvider;

        public UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModels()
        {

        }

        public MainWindowViewModels(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;

            /* Zeigt den View an, der angezeigt werden soll beim Start
             * Könnte man mit einer Startseite verschönern
            */

            BookButton();
        }

        #region Comandas

        public ICommand BookManagement => new RelayCommand(BookManagementButton);
        public ICommand FolderManagement => new RelayCommand(FolderManagementButton);
        public ICommand MouseDrag => new RelayCommand(MouseDownDrag);

        public ICommand CloseApplicationCommand => new RelayCommand(CloseApplication);
        public ICommand LoginApplicationCommand => new RelayCommand(LoginViewCommand);
        public ICommand RegistrationApplicationCommand => new RelayCommand(RegistrationViewCommand);

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        private void BookButton()
        {
            /*
                * Richtigen Konstruktor nutzen!
                * Hier DataContext setzen, damit alle Daten angezeigt werden!
             */

            var bookManagementDataContext = new BookManagementViewModels(
                new AddBookFeature(new BookAddRepository(new NovelNestDataContext())),
                new UpdateBookFeature(new BookUpdateRepository(new NovelNestDataContext())),
                new DialogProvider(),
                new DeleteBookFeature(new BookDeleteRepository(new NovelNestDataContext())));

            CurrentView = new BookManagementView()
            {
                DataContext = bookManagementDataContext
            };
        }

        private void BookManagementButton()
        {
            BookButton();
        }

        private void FolderManagementButton()
        {
            CurrentView = new FolderManagementView();
        }

        public void LoginViewCommand()
        {
            _dialogProvider.ShowMessage("Text", "///////");
        }

        public void RegistrationViewCommand()
        {

        }

        private void MouseDownDrag()
        {
            var loginWindow = Application.Current.MainWindow as Window;

            if (loginWindow is not null && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                loginWindow.DragMove();
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

