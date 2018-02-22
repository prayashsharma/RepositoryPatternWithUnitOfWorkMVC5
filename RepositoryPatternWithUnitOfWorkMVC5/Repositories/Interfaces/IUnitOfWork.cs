using System;

namespace RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int Complete();
    }
}