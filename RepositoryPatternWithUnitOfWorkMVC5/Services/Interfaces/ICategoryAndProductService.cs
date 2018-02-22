using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System.Collections.Generic;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces
{
    public interface ICategoryAndProductService
    {
        void AddCategoryWithProduct(Category category, IList<Product> products);
    }
}