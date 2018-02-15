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
        public CategoryAndProductService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }
        public void AddCategoryWithProduct(Category category, List<Product> products)
        {            
            UnitOfWork.Categories.Add(category);
            foreach (var p in products)
            {
                var product = new Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    Category = category
                };
                UnitOfWork.Products.Add(product);
            }
            UnitOfWork.Complete();
        }
    }
}