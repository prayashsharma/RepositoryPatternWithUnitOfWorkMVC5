using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class ProductService : BaseService, IProductService
    {
        //private readonly IRepository<Product> _productRepository;        

        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {            
            //_productRepository = _unitOfWork.GetRepository<Product>();                        
        }

        //private IRepository<Product> Products
        //{
        //    get { return _productRepository; }
        //}

        public void AddProduct(Product product)
        {
            UnitOfWork.Products.Add(product);
            UnitOfWork.Complete();
        }

        public void EditProduct(Product product)
        {
            UnitOfWork.Products.Edit(product, product.Id);
            UnitOfWork.Complete();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return UnitOfWork.Products.GetAll();
        }

        public IEnumerable<Product> GetAllProductsWithCategory()
        {
            return UnitOfWork.Products.GetAllWithCategory();
        }

        public Product GetProductById(int id)
        {
            return UnitOfWork.Products.Get(id);
        }

        public Product GetProductByName(string name)
        {
            return UnitOfWork.Products.SingleOrDefault(x => x.Name == name);
        }

        public Product GetProductWithCategory(int id)
        {
            return UnitOfWork.Products.GetWithCategory(id);
        }

        public bool IsProductExists(int id)
        {
            return UnitOfWork.Products.Get(id) != null ? true : false;
        }

        public void RemoveProduct(Product product)
        {
            var productToRemove = UnitOfWork.Products.SingleOrDefault(x => x.Id == product.Id);
            if (productToRemove != null)
            {
                UnitOfWork.Products.Remove(productToRemove);
                UnitOfWork.Complete();
            }
        }

    }
}