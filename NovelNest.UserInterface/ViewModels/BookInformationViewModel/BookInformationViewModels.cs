using CommunityToolkit.Mvvm.Input;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelNest.UserInterface.ViewModels.BookInformationViewModel
{
    public class BookInformationViewModels : BaseViewModel
    {
        #region private Felder
        private BookEntity _bookEntity;
        private byte[] _selectedImage;
        private BitmapImage _imageSource;
        #endregion

        public ICommand CloseWindowCommand => new RelayCommand(CloseWindow);
        public Action CloseAction;

        public BookInformationViewModels() { }

        public BookInformationViewModels(BookEntity bookEntity,byte[] selectedImage)
        {
            _bookEntity = bookEntity;
            _selectedImage = selectedImage;
            BookTitle = bookEntity.Title;
            BookDescription = bookEntity.Description;
            BookInformation();
        }

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        private BitmapImage LoadBitImage(byte[] imageData)
        {
            if (imageData is null || imageData.Length is 0)
                return null;

            using (MemoryStream stream = new MemoryStream(imageData))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public BookEntity SelectedBook
        {
            get => _bookEntity;
            set
            {
                if (_bookEntity != value)
                {
                    _bookEntity = value;
                    OnPropertyChanged(nameof(SelectedBook));
                }
            }
        }

        public byte[] SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
                ImageSource = LoadBitImage(_selectedImage);
            }
        }

        public string BookTitle { get; set; }
        public string BookDescription { get; set; }

        private void CloseWindow()
        {
            CloseAction?.Invoke();
        }

        private async Task BookInformation()
        {
            if (SelectedBook == null)
                return;
            else
            {
                SelectedBook.Title = BookTitle;
                SelectedBook.Description = BookDescription;
                ImageSource = LoadBitImage(SelectedBook.ImageBook);
            }
        }
    }
}
