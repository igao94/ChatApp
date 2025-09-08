using Domain.Entites;
using System.Linq.Expressions;

namespace Application.Abstractions.Repositories;

public interface IRepositoryBase<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Delete(T entity);
    Task<T?> GetWithIgnoreQueryFilterAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAllWithIgnoreQueryFilterAsync(Expression<Func<T, bool>> predicate);
}
