using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System.Collections.Generic;

namespace RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetAllWithProducts();        
    }
}