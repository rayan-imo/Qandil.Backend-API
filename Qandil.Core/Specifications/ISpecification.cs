using Qandil.Core.Common;
using System.Linq.Expressions;

namespace Qandil.Core.Specifications
{
    public interface ISpecification<T>where T : BaseEntity
    {
        Expression<Func<T,bool>>? Criteria { get; }

        List<Expression<Func<T,object>>> Includes { get; }

        Expression<Func<T,object>>? OrderBy { get; }

        Expression<Func<T,object>>? OrderByDescending { get; }
        List<(Expression<Func<T, object>> KeySelector, bool Descending)> Orderings { get; }
        public LambdaExpression? Selector { get; }


        int? Take { get; }
        int? Skip { get; }
        public string? CacheKey {  get; }
        public bool AsNoTracking { get; }
        public bool EnableCaching { get; }
        bool IgnoreSoftDeleteFilter { get; }
    }
}

