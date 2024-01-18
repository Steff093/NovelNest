using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IAddBookFeature
{
    public interface IAddBookFeature
    {
        public Task<BookEntity> AddBookAsync(BookEntity book);
    }
}
