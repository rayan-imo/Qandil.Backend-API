using Qandil.Core.Common;
using Qandil.Core.Specifications;
using Qandil.Infrastructure.Service.Expressions;
using System.Linq.Expressions;

namespace Qandil.Infrastructure.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }

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

        public bool IgnoreSoftDeleteFilter { get; protected set; }


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


        public static BaseSpecification<T> Create()
        {
            return new BaseSpecification<T>();
        }

        public BaseSpecification<T> Where(Expression<Func<T, bool>> filter)
        {
            Criteria = filter;
            return this;
        }

        public BaseSpecification<T> AndFilter(Expression<Func<T, bool>> filter)
        {
            if (Criteria == null)
                Criteria = filter;
            else
                Criteria = Combine(Criteria, filter, Expression.AndAlso);
            return this;
        }

        public BaseSpecification<T> OrFilter(Expression<Func<T, bool>> filter)
        {
            if (Criteria == null)
                Criteria = filter;
            else
                Criteria = Combine(Criteria, filter, Expression.OrElse);
            return this;
        }

        public BaseSpecification<T> AndCompositeFilter(BaseSpecification<T> other)
        {
            if (other.Criteria == null) return this;
            if (Criteria == null)
                Criteria = other.Criteria;
            else
                Criteria = Combine(Criteria, other.Criteria, Expression.AndAlso);
            return this;
        }

        public BaseSpecification<T> OrCompositeFilter(BaseSpecification<T> other)
        {
            if (other.Criteria == null) return this;
            if (Criteria == null)
                Criteria = other.Criteria;
            else
                Criteria = Combine(Criteria, other.Criteria, Expression.OrElse);
            return this;
        }


        private Expression<Func<T, bool>> Combine(

           Expression<Func<T, bool>> left,
           Expression<Func<T, bool>> right,
           Func<Expression, Expression, BinaryExpression> combiner)
        {
            var param = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceVisitor(left.Parameters[0], param);
            var rightVisitor = new ReplaceVisitor(right.Parameters[0], param);

            var combined = combiner(leftVisitor.Visit(left.Body), rightVisitor.Visit(right.Body));
            return Expression.Lambda<Func<T, bool>>(combined, param);
        }


        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDesc) => OrderByDescending = orderByDesc;


        protected void ApplyNoTracking() => AsNoTracking = true;

    }
}
