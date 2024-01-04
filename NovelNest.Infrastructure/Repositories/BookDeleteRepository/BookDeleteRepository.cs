using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.BookDeleteRepository
{
    public class BookDeleteRepository : IBookDeleteRepository
    {
        private NovelNestDataContext _context;

        public BookDeleteRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task DeleteBookAsync(BookEntity book)
        {
            try
            {
                _context.BookEntities.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler in DelteRepository " + ex.Message);
            }
        }
    }
}
