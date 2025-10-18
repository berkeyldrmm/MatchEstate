namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T, TKey>
    {
        Task<T> Read(TKey id);
        Task<IEnumerable<T>> ReadAll();
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        bool Delete(T item);
        void DeleteRange(IEnumerable<T> items);
    }
}
