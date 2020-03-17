using System;
using System.Linq.Expressions;

namespace Core.Specifications.Selectors
{
    public interface IPropertySelector<T>
    {
        Expression<Func<T, object>> GetSelector(string propertyName);
    }
}