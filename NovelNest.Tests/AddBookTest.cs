using Moq;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Interfaces.IDialogProvider;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.UserInterface.ViewModels.MainWindowViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Tests
{
    [TestFixture]
    public class AddBookTest
    {
        [Test]
        [TestCase("abc", "abc", false)]
        public async Task AddBook_Validations(string bookName, string bookDescription, bool result)
        {
            // Arrange
            var addBookFeatureMock = new Mock<IAddBookFeature>();
            var updateBookFeatureMock = new Mock<IUpdateBookFeature>();
            var dialogProviderMock = new Mock<IDialogProvider>();
            var deleteBookFeatureMock = new Mock<IDeleteBookFeature>();

            addBookFeatureMock.Setup(mock => mock.AddBookAsync(It.IsAny<BookEntity>())).ReturnsAsync(new BookEntity());

            var viewModel = new MainWindowViewModels(addBookFeatureMock.Object, updateBookFeatureMock.Object, dialogProviderMock.Object, deleteBookFeatureMock.Object);
            viewModel.BookName = bookName;
            viewModel.BookDescription = bookDescription;

            // Act
            await viewModel.AddBook();
            // Assert
            if (result)
            {
                Assert.IsTrue(viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5);
            }
            else
            {
                Assert.IsFalse(viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5);
            }
        }
        [Test]
        public async Task AntoherAddMethod()
        {
            // Arrange
            var addBookFeatureMock = new Mock<IAddBookFeature>();
            var updateBookFeatureMock = new Mock<IUpdateBookFeature>();
            var dialogProviderMock = new Mock<IDialogProvider>();
            var deleteBookFeatureMock = new Mock<IDeleteBookFeature>();
            bool result = false;
            addBookFeatureMock.Setup(mock => mock.AddBookAsync(It.IsAny<BookEntity>())).ReturnsAsync(new BookEntity());

            var viewModel = new MainWindowViewModels(addBookFeatureMock.Object, updateBookFeatureMock.Object, dialogProviderMock.Object, deleteBookFeatureMock.Object);
            viewModel.BookName = "a";
            viewModel.BookDescription = "Nat";


            // Act
            await viewModel.AddBook();
            // Assert
            if (result)
            {
                Assert.IsTrue(viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5);
            }
            else
            {
                Assert.IsFalse(viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5);
            }
        }
        [Test]
        public async Task ThirdTestMethod()
        {
            // Arrange
            var addBookFeatureMock = new Mock<IAddBookFeature>();
            var updateBookFeatureMock = new Mock<IUpdateBookFeature>();
            var dialogProviderMock = new Mock<IDialogProvider>();
            var deleteBookFeatureMock = new Mock<IDeleteBookFeature>();
            bool result = false;
            var viewModel = new MainWindowViewModels(addBookFeatureMock.Object, updateBookFeatureMock.Object, dialogProviderMock.Object, deleteBookFeatureMock.Object);
            viewModel.BookName = "Neuer Buch";
            viewModel.BookDescription = "Ich füge ein neues Buch hinzu!";

            addBookFeatureMock.Setup(mock => mock.AddBookAsync(It.IsAny<BookEntity>()))
                  .ReturnsAsync((BookEntity book) =>
                  {
                      // Hier können Sie prüfen, ob die übergebene BookEntity die erwarteten Werte hat
                      Console.WriteLine($"Expected BookName: {viewModel.BookName}");
                      Console.WriteLine($"Expected BookDescription: {viewModel.BookDescription}");

                      // Ggf. können Sie den Mock entsprechend anpassen, um das erwartete Verhalten zu simulieren
                      return new BookEntity { /* Hier ggf. Daten setzen */ };
                  });
            // Act
            await viewModel.AddBook();
            // Assert

            // Debug-Ausgabe
            Console.WriteLine($"Actual condition: {viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5}");
            Console.WriteLine($"Actual BookName.Length: {viewModel.BookName.Length}");
            Console.WriteLine($"Actual BookDescription.Length: {viewModel.BookDescription.Length}");

            if (result)
            {
                Assert.IsTrue(viewModel.BookName.Length <= 5 && viewModel.BookDescription.Length <= 5);
            }
            else
            {
                Assert.IsFalse(viewModel.BookName.Length >= 5 && viewModel.BookDescription.Length >= 5);
            }
        }
    }
}
