using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SpecificationEvaluator
{
    public static class SpecificationEvaluator<T> where T : Entity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> sourceQuery, ISpecification<T> specification)
        {
            var query = sourceQuery;

            // Filtering
            if (specification.IsFiltersEnabled)
            {
                query = specification.Filters
                    .Aggregate(query, (current, filter) => current.Where(filter));
            }

            // Searching
            // if (specification.IsSearchEnabled)
            // {
            //     query = query.Where(specification.SearchPredicate);
            // }

            // Includes all expression-based includes
            if (specification.IsIncludesEnabled)
            {
                query = specification.Includes
                    .Aggregate(query, (current, include) => current.Include(include));
            }

            // Include all string-based include statements
            if (specification.IsIncludesEnabled)
            {
                query = specification.IncludeStrings
                    .Aggregate(query, (current, include) => current.Include(include));
            }

            // Ordering
            if (specification.IsOrderBysEnabled)
            {
                query = specification.OrderBys
                    .Aggregate(query, (current, orderByInfo) =>
                    {
                        // Item2: isDesc
                        // Descending ordering
                        if (orderByInfo.Item2)
                        {
                            return current.OrderByDescending(orderByInfo.Item1);
                        }
                        // Ascending ordering
                        else
                        {
                            return current.OrderBy(orderByInfo.Item1);
                        }
                    });
            }

            // GroupBy
            // if (specification.IsGroupBysEnabled)
            // {
            //     query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            // }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                int skip = (specification.CurrentPage - 1) * specification.PageSize;
                query = query.Skip(skip).Take(specification.PageSize);
            }

            return query;
        }
    }
}