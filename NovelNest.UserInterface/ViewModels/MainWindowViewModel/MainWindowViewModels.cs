using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using NovelNest.Infrastructure.Repositories.BookUpdateRepository;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
using NovelNest.UserInterface.Views.UpdateView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.MainWindowViewModel
{
    public class MainWindowViewModels : INotifyPropertyChanged
    {
        #region Commands

        public ICommand CloseApplicationCommand => new RelayCommand(ExecuteCloseCommand);
        public ICommand LoginApplicationCOmmand => new RelayCommand(LoginViewCommand);
        public ICommand RegistrationApplicationCommand => new RelayCommand(RegistrationViewCommand);

        #endregion

        #region Button Commands - Hinzufügen, Bearbeiten und Löschen 
        public ICommand UpdateButtonCommand => new RelayCommand(UpdateWindowCommand);
        public ICommand AddNewBookCommand => new RelayCommand(AddBookCommand);
        public ICommand DeleteButtonCommand => new RelayCommand(DeleteBookCommand);

        #endregion

        #region Fields

        private readonly IAddBookFeature<BookEntity> _addBookFeature;
        private readonly IUpdateBookFeature<BookEntity> _updateBookFeature;
        private readonly IDeleteBooKFeature _deleteBookFeature;
        private readonly IDialogProvider _dialogProvider;
        public ObservableCollection<BookEntity> _bookCollection;
        public IDialogProvider DialogProvider { get; set; }
        public BookEntity _bookEntity;

        public MainWindowViewModels()
        {
        }

        public MainWindowViewModels(
            IAddBookFeature<BookEntity> addBookFeature, 
            IUpdateBookFeature<BookEntity> updteBookFeature, 
            IDialogProvider dialogProvider,
            IDeleteBooKFeature delteBookFeature)
        {
            BookCollection = new ObservableCollection<BookEntity>();
            LoadDatabase();
            _addBookFeature = addBookFeature;
            _updateBookFeature = updteBookFeature;
            _dialogProvider = dialogProvider;
            _deleteBookFeature = delteBookFeature;
        }

        public BookEntity SelectedBook
        {
            get => _bookEntity;
            set
            {
                _bookEntity = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public ObservableCollection<BookEntity> BookCollection
        {
            get => _bookCollection;
            set
            {
                _bookCollection = value;
                OnPropertyChanged(nameof(BookCollection));
            }
        }

        private string _bookName;
        private string _bookDescription;

        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }

        public string BookDescription
        {
            get => _bookDescription;
            set
            {
                _bookDescription = value;
                OnPropertyChanged(nameof(BookDescription));
            }
        }
        #endregion

        #region Methoden 

        private void LoadDatabase()
        {
            try
            {
                using (var dbContext = new NovelNestDataContext())
                {
                    BookCollection = new ObservableCollection<BookEntity>(dbContext.BookEntities.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler! " + ex.Message);
            }
        }
        #region Hinzufügen eines Buches

        /* Fügt ein Buch in das Datagrid hinzu
         * Ist eines der Textboxen leer, funktioniert es nicht
         */

        private async Task AddBook()
        {
            try
            {
                if (string.IsNullOrEmpty(BookName) || string.IsNullOrEmpty(BookDescription))
                {
                    _dialogProvider.ShowMessage("Bitte geben Sie einen Buchtitel und eine Beschreibung ein!", "Fehler");
                    BookName = string.Empty;
                    BookDescription = string.Empty;
                    return;
                }

                BookEntity newBook = new BookEntity()
                {
                    Title = BookName,
                    Description = BookDescription,
                };

                var addBook = await _addBookFeature.AddBookAsync(newBook);

                if (addBook is not null)
                {
                    BookCollection.Add(addBook);

                    _dialogProvider.ShowMessage("Buch erfolgreich hinzugefügt!", "Erfolg");

                    BookName = string.Empty;
                    BookDescription = string.Empty;
                }
                else
                    _dialogProvider.ShowError("Es gab einen Fehler beim Hinzufügen des Buches.", "Fehler");

            }
            catch (Exception ex)
            {
                _dialogProvider.ShowError("Ein unerwarteter Fehler ist aufgetreten" + ex.Message, "Fehler!");
            }
        }
        #endregion

        private void AddBookCommand()
        {
            AddBook();
        }

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Command's

        private void ExecuteCloseCommand()
        {
            CloseApplication();
        }

        #region Schließen der Anwendung

        /* Anwendung wird zurzeit nur über das Menü gelöschen
         * ToDo: Auf das X Der Anwendung erweitern
         * Allerdings kann man an das Closing .. "Element(?)" vom MainWindow 
         * keinen Command binden. 
        */

        private void CloseApplication()
        {
            bool result = _dialogProvider.ShowConfirmation("Möchten Sie die Anwendung wirklich schließen?", "NovelNest schließen");

            if (result)
                Application.Current.Shutdown();
            else
                return;
        }
        #endregion

        #region DelteBook Methode

        /* Methode, welches einen Eintrag löscht, wenn etwas ausgewählt wurde
         * Man wird vorher gefragt, ob man sich sicher sei, diesen Eintrag zu löschen.
        */
        private async Task DeleteBook()
        {
            try
            {
                if (SelectedBook is null)
                {
                    _dialogProvider.ShowError("Bitte wähle einen Eintrag aus", "Fehler");
                    return;
                }

                if (SelectedBook is not null)
                {
                    bool reault = _dialogProvider.ShowConfirmation("Möchtest du diesen Eintrag wirklich löschen?", "Hinweis");
                    if (reault)
                    {
                        await _deleteBookFeature.DeleteBookAsync(SelectedBook);
                        BookCollection.Remove(SelectedBook);
                    }
                    else
                        return;

                }

            }
            catch (Exception ex)
            {
                _dialogProvider.ShowError("Es ist ein Fehler aufgetreten! " + ex.Message, "Fehler");
            }
        }
        #endregion

        private void DeleteBookCommand()
        {
            DeleteBook();
        }

        public void LoginViewCommand()
        {
            LoginView view = new();
            view.Show();
        }

        public void RegistrationViewCommand()
        {
            RegistrationView view = new();
            view.Show();
        }

        public void UpdateWindowCommand()
        {
            if (SelectedBook is null)
            {
                _dialogProvider.ShowMessage("Bitte wählen Sie einen Eintrag aus!", "Fehler");
                return;
            }
            var updatebookWindow = new UpdateWindowViewModels(_updateBookFeature, SelectedBook, BookCollection, DialogProvider);
            var view = new UpdateView { DataContext = updatebookWindow };

            updatebookWindow.CloseAction = () => view.Close();

            view.ShowDialog();
        }

        #endregion
    }
}

