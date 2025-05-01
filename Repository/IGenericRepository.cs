using System.Linq.Expressions;

namespace TheraGuide.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> InsertAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task DeleteAsync(object id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task SaveAsync();
    }
}
