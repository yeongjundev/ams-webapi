using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Filters { get; }
        bool IsFiltersEnabled { get; }

        List<Expression<Func<T, object>>> Includes { get; }
        bool IsIncludesEnabled { get; }
        List<string> IncludeStrings { get; }
        bool IsIncludeStringsEnabled { get; }

        List<(Expression<Func<T, object>>, bool)> OrderBys { get; }
        bool IsOrderBysEnabled { get; }

        string SearchKey { get; }
        bool IsSearchEnabled { get; }

        // Expression<Func<T, object>> GroupBy { get; }
        // bool IsGroupBysEnabled { get; }

        int PageSize { get; }
        int CurrentPage { get; }
        bool IsPagingEnabled { get; }
    }
}