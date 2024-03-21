using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.AddBookInterfaceInfrastructure;
using System.Diagnostics;

namespace NovelNest.Infrastructure.Repositories.BookRepositories.BookAddRepository
{
    public class BookAddRepository : IBookAddRepository
    {
        private readonly NovelNestDataContext _context;

        public BookAddRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(BookEntity book)
        {
            try
            {
                _context.BookEntities.Add(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Ausnahmebehandlung
                Debug.WriteLine("Fehler beim Hinzufügen des Buchs: " + ex.Message);

                if (ex.InnerException is not null)
                {
                    Debug.WriteLine("Innere Ausnahme: " + ex.InnerException.Message);
                }
            }
        }
    }
}
