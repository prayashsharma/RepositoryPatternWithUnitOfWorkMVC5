using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;        
        //private Dictionary<Type, object> _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            //_repositories = new Dictionary<Type, object>();
        }

        public IProductRepository Products { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        //public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        //{
        //    if (_repositories.Keys.Contains(typeof(TEntity)))
        //        return _repositories[typeof(TEntity)] as IRepository<TEntity>;

        //    var repository = new Repository<TEntity>(_context);
        //    _repositories.Add(typeof(TEntity), repository);
        //    return repository;
        //}


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}