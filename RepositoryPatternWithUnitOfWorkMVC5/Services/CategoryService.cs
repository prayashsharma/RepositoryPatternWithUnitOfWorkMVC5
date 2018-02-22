using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System.Collections.Generic;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _categoryRepository = UnitOfWork.GetRepository<Category>();
        }

        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository; }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return CategoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return CategoryRepository.Get(id);
        }

        public bool IsCategoryExists(int id)
        {
            return CategoryRepository.Get(id) != null ? true : false;
        }

        public void AddCategory(Category category)
        {
            CategoryRepository.Add(category);
            UnitOfWork.Complete();
        }

        public void EditCategory(Category category)
        {
            CategoryRepository.Edit(category, category.Id);
            UnitOfWork.Complete();
        }

        public void RemoveCategory(Category category)
        {
            var categoryToRemove = CategoryRepository.SingleOrDefault(x => x.Id == category.Id);
            if (categoryToRemove != null)
            {
                CategoryRepository.Remove(categoryToRemove);
                UnitOfWork.Complete();
            }
        }

        public IEnumerable<Category> GetAllCategoriesWithProducts()
        {
            return CategoryRepository.Include(x => x.Products);
        }
    }
}