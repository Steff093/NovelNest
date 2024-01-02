using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
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

        #endregion

        public UpdateWindowViewModels()
        {

        }

        public UpdateWindowViewModels(IUpdateBookFeature<BookEntity> updateBooFeature, BookEntity bookEntity)
        {
            _bookEntity = bookEntity;
            _updateBookFeature = updateBooFeature;
            UpdateBookCommand = new RelayCommand(UpodateBookCommand);
            UpdateBookName = _bookEntity.Title;
            UpdateBookDescription = _bookEntity.Description;
        }

        public Action CloseAction { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UpdateBookName { get; set; }
        public string UpdateBookDescription { get; set; }

        public void UpodateBookCommand()
        {
            if (_updateBookFeature is not null)
            {
                _bookEntity.Title = UpdateBookName;
                _bookEntity.Description = UpdateBookDescription;

                MessageBox.Show("Erfolgreich geändert!");
            }
            else
                throw new Exception("Es ist ein Fehler bei der Veränderung aufgetreten");
        }
    }
}
