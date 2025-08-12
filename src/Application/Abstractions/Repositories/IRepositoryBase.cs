using Domain.Entites;
using System.Linq.Expressions;

namespace Application.Abstractions.Repositories;

public interface IRepositoryBase<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Delete(T entity);
}
