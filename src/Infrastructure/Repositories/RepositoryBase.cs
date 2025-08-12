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

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
}
