using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.UpdateWindowViewModel
{
    public class UpdateWindowViewModels : INotifyPropertyChanged
    {
        #region Variablen

        private BookEntity _bookEntity;
        private readonly IUpdateBookFeature<BookEntity> _updateBookFeature;
        private readonly IDialogProvider _dialogProvider;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand UpdateBookCommand { get; }
        public ICommand CloseUpdateViewCommand { get; }
        public Action CloseAction { get; set; }
        public ObservableCollection<BookEntity> _booksCollection;


        #endregion

        public UpdateWindowViewModels()
        {

        }

        public UpdateWindowViewModels(
            IUpdateBookFeature<BookEntity> updateBookFeature, 
            BookEntity bookEntity, 
            ObservableCollection<BookEntity> booksCollection, 
            IDialogProvider dialogProvider)
        {
            _bookEntity = bookEntity;
            _updateBookFeature = updateBookFeature;
            _booksCollection = booksCollection;
            _dialogProvider = dialogProvider;
            UpdateBookName = bookEntity.Title;
            UpdateBookDescription = bookEntity.Description;
            UpdateBookCommand = new RelayCommand(UpodateBookCommandWrapper);
            CloseUpdateViewCommand = new RelayCommand(CloseUpdateCommand);
            BookCollection = new ObservableCollection<BookEntity>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<BookEntity> BookCollection
        {
            get => _booksCollection;
            set
            {
                if (_booksCollection != value)
                {
                    _booksCollection = value;
                    OnPropertyChanged(nameof(BookCollection));
                }
            }
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

        public string UpdateBookName { get; set; }
        public string UpdateBookDescription { get; set; }

        private void UpodateBookCommandWrapper()
        {
            _ = UpodateBookCommand();
        }

        public async Task UpodateBookCommand()
        {
            if (_updateBookFeature is not null && SelectedBook is not null)
            {
                SelectedBook.Title = UpdateBookName;
                SelectedBook.Description = UpdateBookDescription;

                var updatedBook = await _updateBookFeature.UpdateBookAsync(SelectedBook);

                if (updatedBook is null)
                {
                    return;
                }

                if (updatedBook is not null)
                {
                    _dialogProvider.ShowMessage("Eintrag erfolgreich geändert!", "Update Erfolg");

                    // ToDo: Mit der BookCollection vom MainWindow versuchen, die Liste zu aktualisieren

                    CloseAction.Invoke();
                }
                else
                    _dialogProvider.ShowError("Fehler bei der Aktualisierung!", "Update Fehler");
            }
            else
                _dialogProvider.ShowError("UpdateBookFeature oder SelectedBook ist null", "Update Null Fehler");
        }

        private void CloseUpdateCommand()
        {
            CloseAction?.Invoke();
        }
    }
}
