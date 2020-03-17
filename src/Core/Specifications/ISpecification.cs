using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Filters { get; }

        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }

        List<(Expression<Func<T, object>>, bool)> OrderBys { get; }

        string SearchKey { get; }

        int PageSize { get; }
        int CurrentPage { get; }
        bool IsPagingEnabled { get; }
    }
}