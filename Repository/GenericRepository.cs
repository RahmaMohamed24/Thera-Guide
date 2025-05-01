using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheraGuide.Context;

namespace TheraGuide.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

         private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
        return entity;
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    // Additional helpful methods that could be added:

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(predicate);
    }

    public IQueryable<T> Queryable()
    {
        return _dbSet.AsQueryable();
    }
}
}
