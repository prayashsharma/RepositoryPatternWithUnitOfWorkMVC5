using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool IsCategoryExists(int id);
        void AddCategory(Category category);
        void EditCategory(Category category);
        void RemoveCategory(Category category);
        IEnumerable<Category> GetAllCategoriesWithProducts();

    }
}
