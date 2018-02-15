using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext AppDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public IEnumerable<Product> GetAllWithCategory()
        {
            return AppDbContext.Products.Include(c => c.Category).ToList();
        }

        public Product GetWithCategory(int id)
        {
            return AppDbContext.Products.Include(c => c.Category).SingleOrDefault(p => p.Id == id);
        }
    }
}