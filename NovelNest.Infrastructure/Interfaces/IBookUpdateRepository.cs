namespace NovelNest.Infrastructure.Interfaces
{
    public interface IBookUpdateRepository<T> where T : class
    {
        Task<T> UpdateBookAsync(T book);
    }
}
