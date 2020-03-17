using System;
using System.Collections;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private Hashtable _repositories;

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
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
                        _context
                    );

                _repositories.Add(entityType.Name, newRepository);
            }

            return (IRepository<T>)_repositories[entityType.Name];
        }
    }
}