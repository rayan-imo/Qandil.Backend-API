using Microsoft.EntityFrameworkCore;
using Qandil.Core.Common;
using Qandil.Core.Specifications;

namespace Qandil.Infrastructure.Specifications
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T>  GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            // Soft delete filter
            if (spec.IgnoreSoftDeleteFilter)
                query = query.IgnoreQueryFilters();

            // No tracking
            if (spec.AsNoTracking)
                query = query.AsNoTracking();

            // Criteria (WHERE)
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
             
            // Includes
            query = spec.Includes
                .Aggregate(query, (current, include)
                    => current.Include(include));

            // Ordering (single source of truth)
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // Paging
            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);

            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);

            return query;
        }
    }

}
