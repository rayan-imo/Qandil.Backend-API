using Qandil.Core.Common;
using Qandil.Core.Consts;
using Qandil.Core.Specifications;
using System.Linq.Expressions;

namespace Qandil.Core.Interfacres
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(Guid Id);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> predicate, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip,
            Expression<Func<T, object>> orederBy = null, string OrderByDirection = OrderBy.Ascending);
        //Async
        Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<PagedResult<T>> PagedListAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<T?> GetFirstBySpecAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<T> GetByIdAsync(Guid id, string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<T?> GetByItemAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task HardDeleteAsync(T entity);
        Task HardDeleteRangeAsync(IEnumerable<T> entities);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetBySpecAsync(ISpecification<T> spec);
        public Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(ISpecification<T> spec);
    }


}
