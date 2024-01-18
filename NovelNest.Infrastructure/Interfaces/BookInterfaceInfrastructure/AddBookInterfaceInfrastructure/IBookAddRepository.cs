using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.AddBookInterfaceInfrastructure
{
    public interface IBookAddRepository
    {
        Task AddBookAsync(BookEntity book);
    }
}
