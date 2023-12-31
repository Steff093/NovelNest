
namespace NovelNest.ApplicationLogic.Interfaces
{
    public interface IAddBookFeature<T> where T : class
    {
        public Task<T> AddBookAsync(T book);
    }
}
