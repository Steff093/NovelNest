using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.BookUpdateRepository
{
    public class BookUpdateRepository : IBookUpdateRepository<BookEntity>
    {
        private readonly NovelNestDataContext _context;

        public BookUpdateRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task<BookEntity> UpdateBookAsync(BookEntity book)
        {
            try
            {
                _context.BookEntities.Update(book);
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler in der UpdateBookRepository " + ex.Message);
            }
        }
    }
}
