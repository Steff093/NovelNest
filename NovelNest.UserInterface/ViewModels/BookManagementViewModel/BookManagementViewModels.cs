using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.UpdateView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.BookManagementViewModel
{
    public class BookManagementViewModels 
    {

        #region Button Commands - Hinzufügen, Bearbeiten und Löschen 

        public ICommand UpdateButtonCommand => new RelayCommand(UpdateWindowCommand);
        public ICommand AddNewBookCommand => new RelayCommand(AddBookCommand);
        public ICommand DeleteButtonCommand => new RelayCommand(DeleteBookCommand);

        #endregion

        #region Fields

        private readonly IAddBookFeature _addBookFeature;
        private readonly IUpdateBookFeature _updateBookFeature;
        private readonly IDeleteBookFeature _deleteBookFeature;
        private readonly IDialogProvider _dialogProvider;
        public ObservableCollection<BookEntity> _bookCollection;
        public BookEntity _bookEntity;

        #endregion

        #region Konstruktor 

        public BookManagementViewModels()
        {
        }

        public BookManagementViewModels(
            IAddBookFeature addBookFeature,
            IUpdateBookFeature updteBookFeature,
            IDialogProvider dialogProvider,
            IDeleteBookFeature delteBookFeature)
        {
            BookCollection = new ObservableCollection<BookEntity>();
            LoadDatabase();
            _addBookFeature = addBookFeature;
            _updateBookFeature = updteBookFeature;
            _dialogProvider = dialogProvider;
            _deleteBookFeature = delteBookFeature;
        }

        #endregion

        #region Property

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

        /*
         * Datenbank Liste Laden
         */

        private void LoadDatabase()
        {
            try
            {
                using var dbContext = new NovelNestDataContext();
                BookCollection = new ObservableCollection<BookEntity>(dbContext.BookEntities.ToList());
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowError("Fehler  ", "Es gab folgenden Fehler: " + ex.Message);
                return;
            }
        }

        #region Lösche eines Buches Methode

        /* Methode, welches einen Eintrag löscht, wenn etwas ausgewählt wurde
         * Man wird vorher gefragt, ob man sich sicher sei, diesen Eintrag zu löschen.
        */

        private async Task DeleteBook()
        {
            if (SelectedBook is null)
            {
                _dialogProvider.ShowError("Fehler", "Bitte wähle einen Eintrag aus");
                return;
            }

            if (SelectedBook is not null)
            {
                bool reault = _dialogProvider.ShowQuestionDeleteBook("Hinweis", "Möchtest du diesen Eintrag wirklich löschen?");

                if (reault)
                {
                    await _deleteBookFeature.DeleteBookAsync(SelectedBook);
                    BookCollection.Remove(SelectedBook);
                }
                else
                    return;

            }
        }
        #endregion

        #region Hinzufügen eines Buches

        /* Fügt ein Buch in das Datagrid hinzu
         * Beide Textboxen müssen gefüllt sein
         */

        public async Task AddBook()
        {
            if (string.IsNullOrEmpty(BookName) || string.IsNullOrEmpty(BookDescription))
            {
                _dialogProvider.ShowError("Fehler", "Bitte geben Sie einen Buchtitel und eine Beschreibung ein!");
                BookName = string.Empty; BookDescription = string.Empty;
                return;
            }

            BookEntity newBook = new()
            {
                Title = BookName,
                Description = BookDescription,
            };

            if (BookName.Length < 5 || BookDescription.Length < 5)
            {
                _dialogProvider.ShowError("Fehler", "Ihre Buch und dessen Beschreibung muss mindestens 5 Zeichen lang sein");
                BookName = string.Empty; BookDescription = string.Empty;
                return;
            }

            var addBook = await _addBookFeature.AddBookAsync(newBook);

            if (addBook is not null)
            {
                BookCollection.Add(addBook);
                _dialogProvider.ShowMessage("Erfolg", "Buch erfolgreich hinzugefügt!");
                BookName = string.Empty; BookDescription = string.Empty;
            }
            else
            {
                _dialogProvider.ShowError("Fehler", "Es gab einen Fehler beim Hinzufügen des Buches.");
                return;
            }
        }
        #endregion

        #region UpdateFenster für Notiz

        /* Hier wird geprüft, ob ein Eintrag aus der Liste
         * ausgewählt wurde oder nicht. 
         * Ist dies nicht der Fall, wird eine Fehlermeldung ausgegeben.
         * Ist ein Eintrag ausgewählt, wird das UpdateView geöffnet und
         * man "wechselt" dann zur UpdateWindowViewModel
         */

        public void UpdateBook()
        {
            if (SelectedBook is null)
            {
                _dialogProvider.ShowError("Fehler", "Bitte wählen Sie einen Eintrag aus!");
                return;
            }
            var updatebookWindow = new UpdateWindowViewModels(_updateBookFeature, SelectedBook, BookCollection, _dialogProvider);
            var view = new UpdateView { DataContext = updatebookWindow };

            updatebookWindow.CloseAction = () => view.Close();

            view.ShowDialog();
        }
        #endregion

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Command's Methoden

        private async void AddBookCommand()
        {
            await AddBook();
        }

        private async void DeleteBookCommand()
        {
            await DeleteBook();
        }

        private void UpdateWindowCommand()
        {
            UpdateBook();
        }

        #endregion
    }
}
