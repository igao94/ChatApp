using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly AppDbContext _context = context;

    private readonly DbSet<T> _dbSet = context.Set<T>();

    public void Add(T entity) => _dbSet.Add(entity);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<T?> GetWithIgnoreQueryFilterAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.IgnoreQueryFilters().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithIgnoreQueryFilterAsync()
    {
        return await _dbSet.IgnoreQueryFilters().ToListAsync();
    }
}
