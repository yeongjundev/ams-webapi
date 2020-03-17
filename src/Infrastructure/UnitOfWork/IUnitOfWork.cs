using System;
using Core.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : Entity;

        bool Complete(int minChanges);
    }
}