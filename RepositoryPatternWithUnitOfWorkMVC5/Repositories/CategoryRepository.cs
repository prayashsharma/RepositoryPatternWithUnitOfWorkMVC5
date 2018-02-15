using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext AppDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public IEnumerable<Category> GetAllWithProducts()
        {
            return AppDbContext.Categories.Include(p => p.Products).ToList();
        }
    }
}