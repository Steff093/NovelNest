namespace NovelNest.Infrastructure.Interfaces
{
    public interface IBookAddRepository<T> where T : class
    {
        Task<T> AddBookAsync(T book);
    }
}
