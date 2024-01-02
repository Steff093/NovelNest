using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;

namespace NovelNest.Infrastructure.Repositories.BookAddRepository
{
    public class BookAddRepository : IBookAddRepository<BookEntity>
    {
        private readonly NovelNestDataContext _context;

        public BookAddRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task<BookEntity> AddBookAsync(BookEntity book)
        {
            try
            {
                _context.BookEntities.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {

                throw new Exception("Noch ein Fehler");
            }
        }
    }
}
