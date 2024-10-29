namespace CRMProject.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}
