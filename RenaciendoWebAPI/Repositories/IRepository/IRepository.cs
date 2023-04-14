using System.Linq.Expressions;

namespace RenaciendoWebAPI.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filtro = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
