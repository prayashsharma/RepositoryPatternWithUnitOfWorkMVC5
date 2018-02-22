using RepositoryPatternWithUnitOfWorkMVC5.Models;
using System.Collections.Generic;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductByName(string name);

        bool IsProductExists(int id);

        void AddProduct(Product product);

        void EditProduct(Product product);

        void RemoveProduct(Product product);

        IEnumerable<Product> GetAllProductsWithCategory();

        Product GetProductWithCategory(int id);
    }
}