using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IUpdateBookFeature
{
    public interface IUpdateBookFeature
    {
        public Task<BookEntity> UpdateBookAsync(BookEntity book);
    }
}
