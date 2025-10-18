namespace BusinessLayer.Abstract
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChanges();
    }
}
