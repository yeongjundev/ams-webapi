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
        public List<Expression<Func<T, bool>>> Filters { get; private set; } = new List<Expression<Func<T, bool>>>();
        public bool IsFiltersEnabled { get; private set; } = false;
        protected virtual void ApplyFilter(Expression<Func<T, bool>> selector)
        {
            if (selector == null)
            {
                IsFiltersEnabled = false;
                return;
            }

            Filters.Add(selector);
            IsFiltersEnabled = true;
        }

        // Including (Include)
        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();
        public bool IsIncludesEnabled { get; private set; } = false;
        protected virtual void ApplyInclude(Expression<Func<T, object>> selector)
        {
            if (selector == null)
            {
                IsIncludesEnabled = false;
                return;
            }

            Includes.Add(selector);
            IsIncludesEnabled = true;
        }

        // Including (IncludeString)
        public List<string> IncludeStrings { get; private set; } = new List<string>();
        public bool IsIncludeStringsEnabled { get; private set; } = false;
        protected virtual void ApplyInclude(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                IsIncludeStringsEnabled = false;
                return;
            }

            IncludeStrings.Add(propertyName);
            IsIncludeStringsEnabled = true;
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
            else
            {
                IsSearchEnabled = false;
            }
        }

        // Sorting (OrderBy)
        public List<(Expression<Func<T, object>>, bool)> OrderBys { get; private set; } = new List<(Expression<Func<T, object>>, bool)>();
        public bool IsOrderBysEnabled { get; private set; } = false;
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> selector, bool isDesc)
        {
            if (selector == null)
            {
                IsOrderBysEnabled = false;
                return;
            }

            OrderBys.Add((selector, isDesc));
            IsOrderBysEnabled = true;
        }

        // Grouping (GroupBy)
        // public Expression<Func<T, object>> GroupBy { get; private set; };
        // public bool IsGroupBysEnabled { get; private set; } = false;
        // protected virtual void ApplyGroupBy(Expression<Func<T, object>> selector)
        // {
        //     if (selector == null)
        //     {
        //         IsGroupBysEnabled = false;
        //         return;
        //     }

        //     GroupBy = selector;
        //     IsGroupBysEnabled = true;
        // }

        private readonly int MaxPageSize = 30;
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
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