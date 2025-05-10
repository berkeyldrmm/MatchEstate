namespace BusinessLayer.Abstract
{
    public interface IGenericService<T, TKey> where T : class
    {
        Task<T> GetOne(TKey id);
        Task<IEnumerable<T>> GetAll();
    }
}
