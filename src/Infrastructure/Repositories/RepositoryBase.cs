using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : BaseEntity
{
    private readonly DbSet<T> _context = context.Set<T>();

    public void Add(T entity) => _context.Add(entity);

    public void Delete(T entity) => _context.Remove(entity);

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.ToListAsync();

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _context.FindAsync(id);
}
