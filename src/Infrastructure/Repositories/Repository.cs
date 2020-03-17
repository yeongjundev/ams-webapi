using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Repositories.SpecificationEvaluator;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;

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

        public async ValueTask<QueryResultObject<T>> Find(ISpecification<T> specification = null)
        {
            var qro = new QueryResultObject<T>(specification.PageSize, specification.CurrentPage);
            qro.SetQueryResult(await ApplySpecification(specification, qro).ToListAsync());
            return qro;
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

        private IQueryable<T> ApplySpecification(ISpecification<T> specification, QueryResultObject<T> qro)
        {
            var query = ApplySpecification(specification);

            query = SpecificationEvaluator<T>.GetPagiedQuery(query, specification, qro);
            return query;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (specification != null)
            {
                query = SpecificationEvaluator<T>.GetQuery(query, specification);
            }
            return query;
        }
    }
}