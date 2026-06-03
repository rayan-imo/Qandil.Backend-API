using Qandil.Core.Common;
using Qandil.Core.Specifications;
using System.Linq.Expressions;

namespace Qandil.Infrastructure.Specifications
{
    public class BaseSpecification<T>: ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get;  set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>>? OrderBy { get; protected set; }

        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
        public List<(Expression<Func<T, object>> KeySelector, bool Descending)> Orderings { get; private set; } = [];
        public LambdaExpression? Selector { get; private set; }
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public bool AsNoTracking { get; protected set; }
        public bool EnableCaching { get; private set; } = false;
        public string? CacheKey { get; private set; }

        public bool IgnoreSoftDeleteFilter {  get; protected set; }

        public BaseSpecification<T> UseCache(string cacheKey)
        {
            EnableCaching = true;
            CacheKey = cacheKey;
            return this;
        }
        public BaseSpecification<T> Paginate(int page, int pageSize)
        {
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            return this;
        }
        // Helpers
        protected void ApplyCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;


        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy)=> OrderBy = orderBy;
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDesc) => OrderByDescending = orderByDesc;


        protected void ApplyNoTracking()=> AsNoTracking = true;

    }
}
