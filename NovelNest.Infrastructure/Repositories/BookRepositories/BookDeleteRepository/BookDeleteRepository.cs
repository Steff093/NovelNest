using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.DeleteBookInterfaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.BookRepositories.BookDeleteRepository
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
                throw new Exception("Fehler in DeleteRepository " + ex.Message);
            }
        }
    }
}
