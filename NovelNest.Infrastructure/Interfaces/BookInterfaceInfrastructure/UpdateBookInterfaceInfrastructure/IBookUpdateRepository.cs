using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.UpdateeBookInterfaceInfrastructure
{
    public interface IBookUpdateRepository
    {
        Task UpdateBookAsync(BookEntity book);
    }
}
