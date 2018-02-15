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
        //private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_categoryRepository = _unitOfWork.GetRepository<Category>();
        }

        private IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
        //private IRepository<Category> Categories
        //{
        //    get { return _categoryRepository; }
        //}

        public IEnumerable<Category> GetAllCategories()
        {
            return UnitOfWork.Categories.GetAll();
        }
    }
}
