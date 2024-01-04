using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature
{
    public class UpdateBookFeature : IUpdateBookFeature<BookEntity>
    {
        private readonly IBookUpdateRepository<BookEntity> _bookUpdateRepository;

        public UpdateBookFeature(IBookUpdateRepository<BookEntity> bookUpdateRepository)
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
