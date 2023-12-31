using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Database;

namespace NovelNest.ApplicationLogic.Repositories
{
    public class BookRepository
    {
        private readonly NovelNestDataContext _novelNestdbContext;

        public BookRepository(NovelNestDataContext novelNestDataContext)
        {
            _novelNestdbContext = novelNestDataContext;
        }

        public BookEntity GetById(int id)
        {
            return _novelNestdbContext.BookEntities.Find(id);
        }
    }
}
