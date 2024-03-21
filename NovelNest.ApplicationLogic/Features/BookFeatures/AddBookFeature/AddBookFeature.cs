using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.AddBookInterfaceInfrastructure;
using System.Diagnostics;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature
{
    public class AddBookFeature : IAddBookFeature
    {
        private readonly IBookAddRepository _bookAddRepository;

        public AddBookFeature(IBookAddRepository bookAddRepository)
        {
            _bookAddRepository = bookAddRepository;
        }

        public async Task<BookEntity> AddBookAsync(BookEntity book)
        {
            try
            {
                await _bookAddRepository.AddBookAsync(book);
                return book;
            }
            catch (Exception ex)
            {
                // Ausnahmebehandlung
                Debug.WriteLine("Fehler beim Hinzufügen des Buchs: " + ex.Message);

                if (ex.InnerException is not null)
                {
                    Debug.WriteLine("Innere Ausnahme: " + ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}
