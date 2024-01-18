using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.UpdateeBookInterfaceInfrastructure;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature
{
    public class UpdateBookFeature : IUpdateBookFeature
    {
        private readonly IBookUpdateRepository _bookUpdateRepository;

        public UpdateBookFeature(IBookUpdateRepository bookUpdateRepository)
        {
            _bookUpdateRepository = bookUpdateRepository;
        }

        public async Task<BookEntity> UpdateBookAsync(BookEntity book)
        {
            try
            {
                await _bookUpdateRepository.UpdateBookAsync(book);
                return book;
            }
            catch (Exception)
            {
                throw new Exception("Fehler bei UpdateBookFeature!");
            }
        }
    }
}
