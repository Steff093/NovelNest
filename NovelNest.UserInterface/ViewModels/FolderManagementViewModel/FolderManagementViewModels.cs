using Accessibility;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using NovelNest.ApplicationLogic.Interfaces.FolderInterfaces.FolderAdd;
using NovelNest.ApplicationLogic.Interfaces.FolderInterfaces.FolderDelete;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.UserInterface.ViewModels.BookManagementViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.FolderManagementViewModel
{
    public class FolderManagementViewModels : BaseViewModel
    {
        #region Private Felder
        private Visibility _visibility = Visibility.Collapsed;
        private readonly IDialogProvider _dialogProvider;
        private readonly IFolderAddFeaturecs _folderAddFeature;
        private readonly IFolderDeleteFeatures _folderDeleteFeatures;
        private ObservableCollection<BookEntity> _bookCollection;
        private BookEntity _bookEntity;
        #endregion

        #region Konstruktor
        public FolderManagementViewModels() { }

        public FolderManagementViewModels(IDialogProvider dialogProider,
            IFolderAddFeaturecs folderAddFeatures,
            IFolderDeleteFeatures folderDeleteFeatures)
        {
            LoadDatabase();
            OpenFolderSubMenuForBooks();
            _dialogProvider = dialogProider;
            _folderAddFeature = folderAddFeatures;
            _folderDeleteFeatures = folderDeleteFeatures;
            BookEntity = new BookEntity();
        }
        #endregion

        #region Commands
        public ICommand OpenFolderTextBoxCommand => new RelayCommand(OpenFolderTextBox);
        public ICommand CreateNewFolder => new RelayCommand(CreateNewFolderButton);
        public ICommand DeleteFolder => new RelayCommand(DeleteFolderButton);
        public ICommand OpenSubMenu => new RelayCommand(OpenFolderSubMenuForBooks);
        #endregion

        private bool _isSubMenuVisible;
        public bool IsSubMenuVisible
        {
            get => _isSubMenuVisible;
            set
            {
                if (_isSubMenuVisible != value)
                {
                    _isSubMenuVisible = value;
                    OnPropertyChanged(nameof(IsSubMenuVisible));
                }
            }
        }

        public BookEntity BookEntity
        {
            get => _bookEntity;
            set
            {
                _bookEntity = value;
                OnPropertyChanged(nameof(BookEntity));
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
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

        private FolderEntity _selectFolder;
        public FolderEntity SelectFolder
        {
            get => _selectFolder;
            set
            {
                _selectFolder = value;
                OnPropertyChanged(nameof(SelectFolder));
            }
        }

        public ObservableCollection<BookEntity> Books
        {
            get => _bookCollection;
            set
            {
                _bookCollection = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        private string _folderTextbox;

        public string FolderTextbox
        {
            get => _folderTextbox;
            set
            {
                _folderTextbox = value;
                OnPropertyChanged(nameof(FolderTextbox));
            }
        }

        private void CreateNewFolderButton()
        {
            if (string.IsNullOrEmpty(FolderTextbox))
            {
                _dialogProvider.ShowError("Fehler", "Bitte geben Sie einen Namen für Ihren Ordner an");
                return;
            }

            FolderEntity newFolder = new()
            {
                FolderName = FolderTextbox
            };

            var addFolder = _folderAddFeature.AddFolder(newFolder);

            if (addFolder is null)
                return;
            if (addFolder is not null)
            {
                Folders.Add(newFolder);
                _dialogProvider.ShowMessage("Erfolg", "Ordner wurde erfolgreich erstellt!");
                FolderTextbox = string.Empty;
            }
        }

        private void DeleteFolderButton()
        {
            if (SelectFolder is null)
            {
                _dialogProvider.ShowError("Fehler", "Bitte wählen Sie einen Eintrag aus.");
                return;
            }
            else
            {
                var result = _dialogProvider.ShowQuestionDelete("Löschen", "Nöchten Sie wirklich diesen Ordner löschen?");
                if (result)
                {
                    _dialogProvider.ShowMessage("Erfolg", "Ordner erfolgreich gelöscht");
                    _folderDeleteFeatures.DeleteFolder(SelectFolder);
                    Folders.Remove(SelectFolder);
                }
                else
                    return;
            }
        }

        private void LoadDatabase()
        {
            try
            {
                using var dbContext = new NovelNestDataContext();
                Folders = new ObservableCollection<FolderEntity>(dbContext.FolderEntities.ToList());
            }
            catch (Exception ex)
            {
                _dialogProvider.ShowError("Fehler", "Es gibt einen Fehler bei der Datenbank Verbindung : " + ex.Message);
            }
        }

        // ToDo: Neu Entwicklung zur Auswähl eines Buches aus der Tabelle von BookEntity 

        private void OpenFolderSubMenuForBooks()
        {
            //// Funtkionalität zwischen dem Command und der UI schaffen
            //_dialogProvider.ShowMessage("Erfolg!", "Hier sollte sich ein Menü öffnen!");
            try
            {
                if (SelectFolder is not null)
                {
                    _dialogProvider.ShowMessage("Test", "Test");
                    LoadBooksForSelectFolder();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void OpenFolderTextBox()
        {
            FolderTextbox = string.Empty;
            Visibility = Visibility.Visible;
        }

        // Sollte dafür zuständig sein, Daten von meiner Buch Tabelle anzuzeigen

        private void LoadBooksForSelectFolder()
        {
            if (SelectFolder is not null)
            {
                try
                {
                    using var dbContext = new NovelNestDataContext();
                    SelectFolder = dbContext.FolderEntities
                        .Include(f => f.BookEntities)
                        .FirstOrDefault(f => f.FolderID == SelectFolder.FolderID);
                    Books = new ObservableCollection<BookEntity>(dbContext.BookEntities.ToList());
                }
                catch (Exception ex)
                {
                    _dialogProvider.ShowError("Fehler", "Beim Laden der Bücher ist ein Fehler aufgetreten: " + ex.Message);
                }
            }
        }
    }
}
