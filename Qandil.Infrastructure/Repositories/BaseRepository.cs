using Microsoft.EntityFrameworkCore;
using Qandil.Core.Common;
using Qandil.Core.Consts;
using Qandil.Core.Interfacres;
using Qandil.Core.Specifications;
using Qandil.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace Qandil.Infrastructure.Repositories
{
    public class BaseRepository<T>(DbContext _context) : IBaseRepository<T> where T : BaseEntity
    {

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(Guid Id)
        {
            return _context.Set<T>().Find(Id);
        }
        public T Find(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.SingleOrDefault(predicate);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(predicate).ToList();

        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip)
        {
            return _context.Set<T>().Where(predicate).Take(take).Skip(skip).ToList();
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip,
           Expression<Func<T, object>> orederBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orederBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orederBy);
                else
                    query = query.OrderBy(orederBy);
            }
            return query.ToList();
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _context.Update(entity);
        }

        // Asynchronous 
        public async Task<T?> GetByIdWithAllIncludes(Guid id)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            if (entityType == null) return null;

            IQueryable<T> query = _context.Set<T>();

            // Dynamically include all navigation properties
            foreach (var navigation in entityType.GetNavigations())
            {
                query = query.Include(navigation.Name);
            }

            var keyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();
            if (keyProperty == null) return null;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, keyProperty.Name);
            var constant = Expression.Constant(id);
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public async Task<T> GetByIdAsync(Guid Id, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.SingleOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int take, int skip)
        {
            return await _context.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate,
                int? take = null,
                int? skip = null,
                Expression<Func<T, object>> orderBy = null,
                string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (orderBy != null)
            {
                query = orderByDirection == OrderBy.Ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _context.Update(entity); await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.DeletedAt = DateTime.UtcNow;

            _context.UpdateRange(entities);
        }
        public async Task<T?> GetByItemAsync(Expression<Func<T, bool>>? predicate = null,
            Expression<Func<T, object>>? orderBy = null, bool ascending = true)
        {

            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (orderBy != null)
            {
                query = ascending ?
                    query.OrderBy(orderBy) :
                    query.OrderByDescending(orderBy);
            }


            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByItemAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            return await query.ToListAsync();
        }


        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null)
        {

            IQueryable<T> query = _context.Set<T>().Where(filter);

            bool exist;

            if (filter != null)
            {

                exist = await query.AnyAsync(filter);
            }
            else
            {
                exist = await query.AnyAsync();
            }

            return exist;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(filter);


            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var exist = await query.AnyAsync(filter);

            return exist;
        }


        // Helper: Apply Specification
        private IQueryable<T> ApplySpec(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(
                _context.Set<T>(),
                spec);
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = ApplySpec(spec);
            return await query.ToListAsync(ct);
        }
        public async Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = ApplySpec(spec);
            return await query.FirstOrDefaultAsync(ct);
        }
        public async Task<PagedResult<T>> PagedListAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = ApplySpec(spec);

            // 1- Total count BEFORE paging
            var totalCount = await query.CountAsync(ct);

            // 2- Apply paging safely
            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);

            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);

            // 3- Execute query
            var data = await query.ToListAsync(ct);

            // 4- Calculate paging  
            var pageSize = spec.Take ?? totalCount;
            var pageNumber = spec.Skip.HasValue && spec.Take.HasValue
                ? (spec.Skip.Value / spec.Take.Value) + 1
                : 1;

            //var totalPages = pageSize == 0
            //    ? 0
            //    : (int)Math.Ceiling((double)totalCount / pageSize);

            // 5- Return result
            return new PagedResult<T>(data, pageNumber, pageSize, totalCount);

        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Set<T>()
                .AnyAsync(x => x.Id == id, ct);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task HardDeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task HardDeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TResult>> GetProjectedAsync<TResult>(
            ISpecification<T> spec)
        {
            var query = ApplySpec(spec);

            if (spec is not BaseSpecification<T> baseSpec ||
                baseSpec.Selector == null)
                throw new InvalidOperationException("Selector is required for projection.");

            return await query
                .Select((Expression<Func<T, TResult>>)baseSpec.Selector)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetBySpecAsync(ISpecification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(
                _context.Set<T>(),
                spec);

            return await query.ToListAsync();
        }
    }
}
