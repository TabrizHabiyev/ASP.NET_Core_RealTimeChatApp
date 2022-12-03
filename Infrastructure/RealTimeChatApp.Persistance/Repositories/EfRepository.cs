using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Domain.Entities.Common;
using RealTimeChatApp.Persistance.Contexts;
using System.Linq.Expressions;

public class EfRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
{
    private readonly RealTimeChatAppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(RealTimeChatAppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        var query = _dbSet.AsQueryable();
        foreach (var propertySelector in propertySelectors)
        {
            query = query.Include(propertySelector);
        }
        return query;
    }

    public async Task<List<TEntity>> GetAllList()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        var query = _dbSet.AsQueryable();
        foreach (var propertySelector in propertySelectors)
        {
            query = query.Include(propertySelector);
        }
        return await query.ToListAsync();
    }

    public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task<TEntity> Get(TPrimaryKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(TPrimaryKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWhere(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<bool> All(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AllAsync(predicate);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
