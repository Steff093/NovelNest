namespace NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature
{
    public interface IUpdateBookFeature<T> where T : class
    {
        public Task<T> UpdateBookAsync(T book);
    }
}
