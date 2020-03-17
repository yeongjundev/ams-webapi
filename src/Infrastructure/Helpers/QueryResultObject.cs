using System.Collections.Generic;
using Core.Entities;

namespace Infrastructure.Helpers
{
    public class QueryResultObject<T> where T : Entity
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int LastPage { get; set; } = 1;

        public int TotalCount { get; set; } = 0;

        public bool IsPaged { get; set; }

        public List<T> QueryResult { get; private set; }

        public QueryResultObject(int pageSize, int currentPage)
        {
            PageSize = pageSize > 0 ? pageSize : 1;
            CurrentPage = currentPage > 0 ? currentPage : 1;
        }

        public void SetQueryResult(List<T> result)
        {
            QueryResult = result;
        }
    }
}