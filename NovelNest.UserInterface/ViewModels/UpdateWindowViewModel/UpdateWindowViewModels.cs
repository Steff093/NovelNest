using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.Domain.Entities.BookEntities;
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
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand UpdateBookCommand { get; }
        public ICommand CloseUpdateViewCommand { get; }

        #endregion

        public UpdateWindowViewModels()
        {

        }

        public UpdateWindowViewModels(IUpdateBookFeature<BookEntity> updateBookFeature, BookEntity bookEntity)
        {
            _bookEntity = bookEntity;
            _updateBookFeature = updateBookFeature;
            UpdateBookName = bookEntity.Title;
            UpdateBookDescription = bookEntity.Description;
            UpdateBookCommand = new RelayCommand(UpodateBookCommandWrapper);
            CloseUpdateViewCommand = new RelayCommand(CloseUpdateCommand);
        }

        public Action CloseAction { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public void UpodateBookCommandWrapper()
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
                    MessageBox.Show("Erfolgreich geändert!", "Erfolg",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    CloseAction.Invoke();

                }
                else
                    MessageBox.Show("Fehler beim Aktualisieren des Buches!");
            }
            else
                MessageBox.Show("UpdateBookFeature oder SelectedBook ist null!");
        }

        private void CloseUpdateCommand()
        {
            CloseAction?.Invoke();
        }
    }
}
