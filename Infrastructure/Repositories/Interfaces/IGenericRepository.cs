using Domain.Common;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface IGenericRepository<T> where T : notnull, BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
