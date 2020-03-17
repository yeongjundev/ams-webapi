using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Repositories.SpecificationEvaluator;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public ValueTask<int> Count(ISpecification<T> specification = null)
        {
            return new ValueTask<int>(ApplySpecification(specification).CountAsync());
        }

        public ValueTask<T> Find(params object[] ids)
        {
            return _dbContext.Set<T>().FindAsync(ids);
        }

        public ValueTask<List<T>> Find(ISpecification<T> specification = null)
        {
            return new ValueTask<List<T>>(ApplySpecification(specification).ToListAsync());
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (specification != null)
            {
                return SpecificationEvaluator<T>.GetQuery(query, specification);
            }
            return query;
        }
    }
}