using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces;
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
        #region Variablen

        public ICommand? CloseApplicationCommand { get; }
        public ICommand? LoginApplicationCOmmand { get; }
        public ICommand? RegistrationApplicationCommand { get; }
        public ICommand? UpdateApplicationWindowCommand { get; }
        public ICommand? AddBookCommand => new RelayCommand(AddBook);

        private readonly IAddBookFeature<BookEntity> _addBookFeature;
        private readonly IUpdateBookFeature<BookEntity> _updateBookFeature;
        private ObservableCollection<BookEntity> _bookCollection;
        private BookEntity _bookEntity;

        #endregion

        public MainWindowViewModels()
        {
        }
        public MainWindowViewModels(IAddBookFeature<BookEntity> addBookFeature, IUpdateBookFeature<BookEntity> updteBookFeature)
        {
            BookCollection = new ObservableCollection<BookEntity>();
            LoadDatabase();
            _addBookFeature = addBookFeature;
            _updateBookFeature = updteBookFeature;
            CloseApplicationCommand = new RelayCommand(ExecuteCloseCommand);
            LoginApplicationCOmmand = new RelayCommand(LoginViewCommand);
            UpdateApplicationWindowCommand = new RelayCommand(UpdateWindowCommand);
        }

        // SelectedBook stellt einen ausgewähltes Element im DataGrid dar.

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

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

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

        private async void AddBook()
        {
            try
            {
                if (string.IsNullOrEmpty(BookName) || string.IsNullOrEmpty(BookDescription))
                {
                    MessageBox.Show("Bitte geben Sie einen Buchtitel und eine Beschreibung ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show(
                        "Buch erfolgreich hinzugefügt!", "Erfolg",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Es gab einen Fehler beim Hinzufügen des Buches.", "Fehler",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ein unerwarteter Fehler ist aufgetreten: " + ex.Message, "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteCloseCommand()
        {
            CloseApplication();
        }

        private void CloseApplication()
        {
            MessageBoxResult result = MessageBox.Show(
            "Möchten Sie die Anwendung wirklich schließen?",
            "NovelNest schließen",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    return;
            }
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
                MessageBox.Show("Bitte wählen Sie einen Eintrag aus!");
                return;
            }
            var updatebookWindow = new UpdateWindowViewModels(_updateBookFeature, SelectedBook);
            var view = new UpdateView { DataContext = updatebookWindow };

            updatebookWindow.CloseAction = () => view.Close();

            view.ShowDialog();
        }
    }
}

