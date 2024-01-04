using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
using Moq;
using NovelNest.Domain.Entities.BookEntities;
using System.Collections.ObjectModel;
using NovelNest.ApplicationLogic.Interfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDeleteBookFeature;
namespace NovelNest.Tests
{
    public class Tests
    {
        [Test]
        public void AddBookCommandCompareWhetherTheBookList()
        {
            // Arrange
            var addBookFeatureMock = new Mock<IAddBookFeature<BookEntity>>();
            var updateBookFeatureMock = new Mock<IUpdateBookFeature<BookEntity>>();
            var dialogProviderMock = new Mock<IDialogProvider>();
            var deleteBookFeatureMock = new Mock<IDeleteBooKFeature>();

            var viewModel = new MainWindowViewModels(addBookFeatureMock.Object, updateBookFeatureMock.Object, dialogProviderMock.Object, deleteBookFeatureMock.Object);

            viewModel.BookName = "Test Book";
            viewModel.BookDescription = "Test Description";

            addBookFeatureMock.Setup(ab => ab.AddBookAsync(It.IsAny<BookEntity>())).ReturnsAsync(new BookEntity { Title = "Test Book", Description = "Test Description" });
            viewModel.AddNewBookCommand.Execute(null);
           
            // Assert
            Assert.AreEqual("NeuesBuch1", viewModel.BookCollection[0].Title, "Das hinzugefügte Buch hat den richtigen Titel");
            Assert.AreEqual("BuchIstNeu1", viewModel.BookCollection[0].Description, "Das hinzugefügte Buch hat die richtige Beschreibung");
        }
    }
}