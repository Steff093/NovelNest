using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature
{
    public interface IDeleteBookFeature
    {
        public Task DeleteBookAsync(BookEntity book);
    }
}
