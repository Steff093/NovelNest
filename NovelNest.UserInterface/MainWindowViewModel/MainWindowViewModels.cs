using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.RegistrationView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using NovelNest.Infrastructure.Database;
using NovelNest.ApplicationLogic.Features.BookFeatures;

namespace NovelNest.UserInterface.MainWindowViewModel
{
    public class MainWindowViewModels : INotifyPropertyChanged
    {
        #region Variablen

        public ICommand? CloseApplicationCommand { get; }
        public ICommand? LoginApplicationCOmmand { get; }
        public ICommand? RegistrationApplicationCommand { get; }
        public ICommand? AddBookCommand => new RelayCommand(AddBook);
        private readonly IAddBookFeature<BookEntity> _addBookFeature;
        private ObservableCollection<BookEntity> _bookCollection;
        private string _bookName;
        private string _bookDescription;

        #endregion
        public ObservableCollection<BookEntity> BookCollection
        {
            get { return _bookCollection; }
            set
            {
                _bookCollection = value;
                OnPropertyChanged(nameof(BookCollection));
            }
        }

        public MainWindowViewModels()
        {
            BookCollection = new ObservableCollection<BookEntity>();
            LoadDatabase();
        }
        public MainWindowViewModels(IAddBookFeature<BookEntity> addBookFeature)
        {
            _addBookFeature = addBookFeature;
            CloseApplicationCommand = new RelayCommand(ExecuteCloseCommand);
            LoginApplicationCOmmand = new RelayCommand(LoginViewCommand);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                BookEntity newBook = new BookEntity()
                {
                    Title = BookName,
                    Description = BookDescription,
                };
                var addBook = await _addBookFeature.AddBookAsync(newBook);
                BookCollection.Add(addBook);
                if (addBook is null)
                {
                    return;
                }
                if (addBook is not null)
                {
                    if (!string.IsNullOrEmpty(BookName) && !string.IsNullOrEmpty(BookDescription))
                    {
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                else
                {
                    MessageBox.Show("Error two");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.ToString());
            }
        }

        private void ExecuteCloseCommand()
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
    }
}

