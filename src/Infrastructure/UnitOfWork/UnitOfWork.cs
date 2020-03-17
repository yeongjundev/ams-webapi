using System;
using System.Collections;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Hashtable _repositories;

        public bool Complete(int minChanges)
        {
            var numChanges = _dbContext.SaveChanges();
            if (minChanges > numChanges)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityType = typeof(T);

            if (!_repositories.ContainsKey(entityType.Name))
            {
                var repositoryType = typeof(Repository<>);
                var newRepository = Activator
                    .CreateInstance(
                        repositoryType.MakeGenericType(entityType),
                        _dbContext
                    );

                _repositories.Add(entityType.Name, newRepository);
            }

            return (IRepository<T>)_repositories[entityType.Name];
        }
    }
}