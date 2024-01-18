using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.DeleteBookInterfaceInfrastructure;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.DeleteBookFeature
{
    public class DeleteBookFeature : IDeleteBookFeature
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
