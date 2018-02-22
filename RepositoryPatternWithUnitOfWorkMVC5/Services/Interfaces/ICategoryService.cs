using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System.Collections.Generic;

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