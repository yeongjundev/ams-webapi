using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        ValueTask<T> Find(params object[] ids);
        ValueTask<List<T>> Find(ISpecification<T> specification = null);

        void Add(T entity);
        void AddRange(List<T> entities);

        void Remove(T entity);
        void RemoveRange(List<T> entities);

        void Update(T entity);

        ValueTask<int> Count(ISpecification<T> specification = null);
    }
}