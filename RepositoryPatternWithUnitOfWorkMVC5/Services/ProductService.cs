using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _productRepository = UnitOfWork.GetRepository<Product>();
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository; }
        }

        public void AddProduct(Product product)
        {
            ProductRepository.Add(product);
            UnitOfWork.Complete();
        }

        public void EditProduct(Product product)
        {
            ProductRepository.Edit(product, product.Id);
            UnitOfWork.Complete();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }

        public IEnumerable<Product> GetAllProductsWithCategory()
        {
            return ProductRepository.Include(x => x.Category);
        }

        public Product GetProductById(int id)
        {
            return ProductRepository.Get(id);
        }

        public Product GetProductByName(string name)
        {
            return ProductRepository.SingleOrDefault(x => x.Name == name);
        }

        public Product GetProductWithCategory(int id)
        {
            return ProductRepository.Include(x => x.Category).SingleOrDefault(x => x.Id == id);
        }

        public bool IsProductExists(int id)
        {
            return ProductRepository.Get(id) != null ? true : false;
        }

        public void RemoveProduct(Product product)
        {
            var productToRemove = ProductRepository.Get(product.Id);
            if (productToRemove != null)
            {
                ProductRepository.Remove(productToRemove);
                UnitOfWork.Complete();
            }
        }
    }
}