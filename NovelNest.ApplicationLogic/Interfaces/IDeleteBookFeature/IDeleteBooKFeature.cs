using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.ApplicationLogic.Interfaces.IDeleteBookFeature
{
    public interface IDeleteBooKFeature
    {
        public Task DeleteBookAsync(BookEntity book);
    }
}
