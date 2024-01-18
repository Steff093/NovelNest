using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.UpdateeBookInterfaceInfrastructure;

namespace NovelNest.Infrastructure.Repositories.BookRepositories.BookUpdateRepository
{
    public class BookUpdateRepository : IBookUpdateRepository
    {
        private readonly NovelNestDataContext _context;

        public BookUpdateRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task UpdateBookAsync(BookEntity book)
        {
            try
            {
                _context.BookEntities.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler in der UpdateBookRepository " + ex.Message);
            }
        }
    }
}
