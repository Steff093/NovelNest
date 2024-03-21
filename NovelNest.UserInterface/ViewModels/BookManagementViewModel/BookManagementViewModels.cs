using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.UserInterface.ViewModels.UpdateWindowViewModel;
using NovelNest.UserInterface.Views.LoginView;
using NovelNest.UserInterface.Views.UpdateView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelNest.UserInterface.ViewModels.BookManagementViewModel
{
    public class BookManagementViewModels : BaseViewModel
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB in Bytes
        #region Button Commands - Hinzufügen, Bearbeiten und Löschen 

        public ICommand UpdateButtonCommand => new RelayCommand(UpdateWindowCommand);
        public ICommand AddNewBookCommand => new RelayCommand(AddBookCommand);
        public ICommand DeleteButtonCommand => new RelayCommand(DeleteBookCommand);
        public ICommand MouseDoubleClickCommand => new RelayCommand(BookMouseDoubleClickEventCommand);
        public ICommand PictureFileOpenCommand => new RelayCommand(OpenPictureForBookCommand);

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
            LoadDatabase();
            _addBookFeature = addBookFeature;
            _updateBookFeature = updteBookFeature;
            _dialogProvider = dialogProvider;
            _deleteBookFeature = delteBookFeature;
            _bookEntity = new BookEntity();
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

        private ObservableCollection<FolderEntity> _folders;

        public ObservableCollection<FolderEntity> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged(nameof(Folders));
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

        private Visibility _imageSelected = Visibility.Visible;
        public Visibility ImageSelected
        {
            get => _imageSelected;
            set
            {
                _imageSelected = value;
                OnPropertyChanged(nameof(ImageSelected));
            }
        }

        private Visibility _labelVisibility = Visibility.Visible;
        public Visibility LabelVisibility
        {
            get => _labelVisibility;
            set
            {
                _labelVisibility = value;
                OnPropertyChanged(nameof(LabelVisibility));
            }
        }

        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set
            {
                if (value == null)
                    return;
                else
                {
                    _selectedImage = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }
        private bool _isPictureAvailable = false;
        public bool IsPictureAvailable
        {
            get => _isPictureAvailable;
            set
            {
                if (SelectedImage != null)
                {
                    _isPictureAvailable = value;
                    OnPropertyChanged(nameof(IsPictureAvailable));
                }
                else
                {
                    _isPictureAvailable = false;
                    OnPropertyChanged(nameof(IsPictureAvailable));
                }
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
                BookCollection = new ObservableCollection<BookEntity>(
                    dbContext.BookEntities
                        .Select(book => new BookEntity
                        {
                            BookId = book.BookId,
                            Title = book.Title,
                            Description = book.Description,
                            ImageBook = book.ImageBook,
                            ImageMIMEType = book.ImageMIMEType,
                            IsPictureAvailable = book.ImageBook != null,
                        })
                        .ToList());
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowError("Fehler", "Hier ist ein Fehler: " + ex.Message.ToString());
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
            else
            {
                bool reault = _dialogProvider.ShowQuestionDelete("Hinweis", "Möchtest du diesen Eintrag wirklich löschen?");

                if (reault)
                {
                    _dialogProvider.ShowMessage("Erfolg", "Eintrag erfolgreich entfernt");
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
                ImageBook = SelectedBook.ImageBook,
                ImageMIMEType = SelectedBook.ImageMIMEType,
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
                BookName = string.Empty; BookDescription = string.Empty; LabelVisibility = Visibility.Visible; ImageSelected = Visibility.Collapsed ;
            }
            else
            {
                _dialogProvider.ShowError("Fehler", "Es gab einen Fehler beim Hinzufügen des Buches.");
                return;
            }
        }
        #endregion

        #region UpdateFenster für den Bucheintrag

        /* Hier wird geprüft, ob ein Eintrag aus der Liste
         * ausgewählt wurde oder nicht. 
         * Ist dies nicht der Fall, wird eine Fehlermeldung ausgegeben.
         * Ist ein Eintrag ausgewählt, wird das UpdateView geöffnet und
         * man "wechselt" dann zur UpdateWindowViewModel
         */

        public async Task UpdateBook()
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

        #region DoubleClick für DataGrid 

        private void BookMouseDoubleClickEvent()
        {
            if (SelectedBook is null)
            {
                return;
            }
            else
            {
                _dialogProvider.ShowMessage("Hinweis", "Hier entsteht bald etwas neues"); 
            }
        }

        #endregion
        private async Task OpenPictureForBook()
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();

            openfileDialog.Filter = "Image Files (*.png; *.jpg) | *.png; *.jpg";
            openfileDialog.FilterIndex = 2;
            openfileDialog.Title = "Wählen Sie ein Buch-Cover";

            bool? result = openfileDialog.ShowDialog();

            if (result == true)
            {
                FileInfo fileInfo = new FileInfo(openfileDialog.FileName);

                if (fileInfo.Length > MaxFileSize)
                {
                    _dialogProvider.ShowError("Fehler", "Ihr Bild ist zu groß. Bitte wählen Sie eine kleinere Datei");
                    return;
                }
                SelectedImage = new BitmapImage(new Uri(openfileDialog.FileName));
                ImageSelected = Visibility.Visible;
                LabelVisibility = Visibility.Collapsed;

                using (Stream fileStream = openfileDialog.OpenFile())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);

                        byte[] imageData = memoryStream.ToArray();

                        string mimeType = MimeType(openfileDialog.FileName);

                        if (SelectedImage is not null)
                        {
                            SelectedBook.ImageBook = imageData;
                            SelectedBook.ImageMIMEType = mimeType;
                            IsPictureAvailable = true;
                        }
                        else
                            IsPictureAvailable = false;
                    }
                }
            }
            else
            {
                LabelVisibility = Visibility.Visible;
            }
        }

        private string MimeType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".png": return "image/png";
                case ".jpg": case ".jpeg": return "image/jpeg";
                default: return "application/octet-stream";
            }
        }

        #endregion

        #region Command's Methoden

        private void AddBookCommand()
        {
            AddBook();
        }

        private void DeleteBookCommand()
        {
            DeleteBook();
        }

        private void UpdateWindowCommand()
        {
            UpdateBook();
        }

        private void BookMouseDoubleClickEventCommand()
        {
            BookMouseDoubleClickEvent();
        }

        private void OpenPictureForBookCommand()
        {
            OpenPictureForBook();
        }

        #endregion
    }
}
