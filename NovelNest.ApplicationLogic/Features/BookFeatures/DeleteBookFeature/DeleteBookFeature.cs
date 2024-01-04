using NovelNest.ApplicationLogic.Interfaces.IDeleteBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature
{
    public class DeleteBookFeature : IDeleteBooKFeature
    {
        private readonly IBookDeleteRepository _deleteBookRepository;

        public DeleteBookFeature(IBookDeleteRepository deleteBookRepository)
        {
            _deleteBookRepository = deleteBookRepository;
        }

        public async Task DeleteBookAsync(BookEntity book)
        {
            try
            {
                await _deleteBookRepository.DeleteBookAsync(book);
            }
            catch (Exception ex)
            {

                throw new Exception("Fehler " + ex.Message);
            }
        }
    }
}
