using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.AddBookInterfaceInfrastructure;

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
            catch (Exception)


            {
                throw new Exception("Error");
            }
        }
    }
}
