﻿using CommunityToolkit.Mvvm.Input;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NovelNest.UserInterface.ViewModels.UpdateWindowViewModel
{
    public class UpdateWindowViewModels : BaseViewModel
    {
        #region Fields
        private BookEntity _bookEntity;
        private readonly IUpdateBookFeature _updateBookFeature;
        private readonly IDialogProvider _dialogProvider;
        #endregion

        #region ICommand + Action
        public ICommand UpdateBookCommand { get; }
        public ICommand CloseUpdateViewCommand { get; }
        public Action CloseAction { get; set; }
        #endregion

        #region Konstruktoren 
        public UpdateWindowViewModels()
        {

        }

        public UpdateWindowViewModels(
            IUpdateBookFeature updateBookFeature, 
            BookEntity bookEntity, 
            IDialogProvider dialogProvider)
        {
            _bookEntity = bookEntity;
            _updateBookFeature = updateBookFeature;
            _dialogProvider = dialogProvider;
            UpdateBookName = bookEntity.Title;
            UpdateBookDescription = bookEntity.Description;
            UpdateBookCommand = new RelayCommand(UpodateBookCommandWrapper);
            CloseUpdateViewCommand = new RelayCommand(CloseUpdateCommand);
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

        public string UpdateBookName { get; set; }
        public string UpdateBookDescription { get; set; }

        #endregion

        #region CommmandsMethoden

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
                    return;

                if (updatedBook is not null)
                {
                    _dialogProvider.ShowMessage("Update Erfolg", "Eintrag erfolgreich geändert!");
                    CloseAction.Invoke();
                }
                else
                    _dialogProvider.ShowError("Update Fehler", "Fehler bei der Aktualisierung!");
            }
            else
                _dialogProvider.ShowError("Update Null Fehler", "UpdateBookFeature oder SelectedBook ist null");
        }

        private void CloseUpdateCommand()
        {
            CloseAction?.Invoke();
        }
        #endregion
    }
}
