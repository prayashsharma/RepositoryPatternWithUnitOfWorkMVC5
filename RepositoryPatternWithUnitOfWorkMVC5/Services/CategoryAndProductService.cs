using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class CategoryAndProductService : BaseService, ICategoryAndProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryAndProductService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _categoryRepository = UnitOfWork.GetRepository<Category>();
            _productRepository = UnitOfWork.GetRepository<Product>();
        }

        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository; }
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository; }
        }

        public void AddCategoryWithProduct(Category category, IList<Product> products)
        {
            CategoryRepository.Add(category);
            foreach (var p in products)
            {
                var product = new Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    Category = category
                };
                ProductRepository.Add(product);
            }
            UnitOfWork.Complete(); 
        }
    }
}