using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;        

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        private IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return UnitOfWork.Categories.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return UnitOfWork.Categories.Get(id);
        }

        public bool IsCategoryExists(int id)
        {
            return UnitOfWork.Categories.Get(id) != null ? true : false;
        }

        public void AddCategory(Category category)
        {
            UnitOfWork.Categories.Add(category);
            UnitOfWork.Complete();
        }

        public void EditCategory(Category category)
        {
            UnitOfWork.Categories.Edit(category, category.Id);
            UnitOfWork.Complete();
        }

        public void RemoveCategory(Category category)
        {
            var categoryToRemove = UnitOfWork.Categories.SingleOrDefault(x => x.Id == category.Id);
            if (categoryToRemove != null)
            {
                UnitOfWork.Categories.Remove(categoryToRemove);
                UnitOfWork.Complete();
            }
        }

        public IEnumerable<Category> GetAllCategoriesWithProducts()
        {
            return UnitOfWork.Categories.GetAllWithProducts();
        }
    }
}
