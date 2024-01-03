namespace NovelNest.ApplicationLogic.Interfaces
{
    public interface IUpdateBookFeature<T> where T : class
    {
        public Task<T> UpdateBookAsync(T book);
    }
}
