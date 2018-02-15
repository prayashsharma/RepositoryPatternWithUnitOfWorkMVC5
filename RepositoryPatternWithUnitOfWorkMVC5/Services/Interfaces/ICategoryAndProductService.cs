using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces
{
    public interface ICategoryAndProductService
    {
        void AddCategoryWithProduct(Category category, List<Product> products);
    }
}
