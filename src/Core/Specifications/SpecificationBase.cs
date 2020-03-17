using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications
{
    public abstract class SpecificationBase<T> : ISpecification<T> where T : Entity
    {
        protected readonly IPropertySelector<T> PropertySelector;

        public SpecificationBase() { }

        public SpecificationBase(string searchKey, (string, bool)[] orderByPropertyNames, int currentPage, int pageSize)
        {
            ApplySearch(searchKey);

            foreach (var (propertyName, isDesc) in orderByPropertyNames)
            {
                ApplyOrderBy(PropertySelector.GetSelector(propertyName), isDesc);
            }

            ApplyPaging(currentPage, pageSize);
        }


        // Filtering (Where)
        public List<Expression<Func<T, bool>>> Filters { get; private set; }
        public bool IsFiltersEnabled { get; private set; } = false;
        protected virtual void ApplyFilter(Expression<Func<T, bool>> selector)
        {
            if (selector == null)
            {
                return;
            }

            if (Filters == null)
            {
                Filters = new List<Expression<Func<T, bool>>>();
                IsFiltersEnabled = true;
            }
            Filters.Add(selector);
        }

        // Including (Include)
        public List<Expression<Func<T, object>>> Includes { get; private set; }
        public bool IsEncludesEnabled { get; private set; } = false;
        protected virtual void ApplyInclude(Expression<Func<T, object>> selector)
        {
            if (selector == null)
            {
                return;
            }

            if (Includes == null)
            {
                Includes = new List<Expression<Func<T, object>>>();
                IsEncludesEnabled = true;
            }
            Includes.Add(selector);
        }

        // Including (IncludeString)
        public List<string> IncludeStrings { get; private set; }
        public bool IsEncludeStringsEnabled { get; private set; } = false;
        protected virtual void ApplyInclude(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (IncludeStrings == null)
            {
                IncludeStrings = new List<string>();
                IsEncludeStringsEnabled = true;
            }
            IncludeStrings.Add(propertyName);
        }

        // Search (Like)
        public string SearchKey { get; private set; }
        public bool IsSearchEnabled { get; private set; } = false;
        protected virtual void ApplySearch(string searchKey)
        {
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                SearchKey = searchKey;
                IsSearchEnabled = true;
            }
        }

        // Sorting (OrderBy)
        public List<(Expression<Func<T, object>>, bool)> OrderBys { get; private set; }
        public bool IsOrderBysEnabled { get; private set; } = false;
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> selector, bool isDesc)
        {
            if (selector == null)
            {
                return;
            }

            if (OrderBys == null)
            {
                OrderBys = new List<(Expression<Func<T, object>>, bool)>();
                IsOrderBysEnabled = true;
            }
            OrderBys.Add((selector, isDesc));
        }

        // Grouping (GroupBy)
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public bool IsGroupBysEnabled { get; private set; } = false;
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> selector)
        {
            if (selector != null && GroupBy == null)
            {
                GroupBy = selector;
                IsGroupBysEnabled = true;
            }
        }

        private readonly int MaxPageSize = 30;
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        protected virtual void ApplyPaging(int currentPage, int pageSize)
        {
            if (currentPage > 0 && pageSize > 0)
            {
                CurrentPage = currentPage;
                PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            }
            else
            {
                currentPage = 1;
                PageSize = MaxPageSize;
            }
            IsPagingEnabled = true;
        }
    }
}