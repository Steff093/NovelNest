namespace NovelNest.ApplicationLogic.Interfaces.IAddBookFeature
{
    public interface IAddBookFeature<T> where T : class
    {
        public Task<T> AddBookAsync(T book);
    }
}
