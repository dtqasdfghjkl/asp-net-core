using System.Linq.Expressions;
using WebApplication1.Common;

namespace WebApplication1.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAll(List<Expression<Func<T, object>>> includeExpressions, CancellationToken cancellationToken = default);
        Task<PaginatedResult<T>> GetPaginatedData(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<PaginatedResult<T>> GetPaginatedData(int pageNumber, int pageSize, Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        Task<PaginatedResult<T>> GetPaginatedData(int pageNumber, int pageSize, Expression<Func<T, bool>> filter, string sortBy, string sortOrder, CancellationToken cancellationToken = default);
        Task<PaginatedResult<T>> GetPaginatedData(List<Expression<Func<T, object>>> includeExpressions, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<T?> GetById<Tid>(Tid id, CancellationToken cancellationToken = default);
        Task<T?> GetById<Tid>(List<Expression<Func<T, object>>> includeExpressions, Tid id, CancellationToken cancellationToken = default);
        Task<bool> IsExists<Tvalue>(string key, Tvalue value, CancellationToken cancellationToken = default);
        Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value, CancellationToken cancellationToken = default);
        Task<T> Create(T model, CancellationToken cancellationToken = default);
        Task CreateRange(List<T> model, CancellationToken cancellationToken = default);
        Task Update(T model, CancellationToken cancellationToken = default);
        Task Delete(T model, CancellationToken cancellationToken = default);
        Task SaveChangeAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
