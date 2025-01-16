namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetOne(string id);
        Task<IEnumerable<T>> GetAll();
    }
}
