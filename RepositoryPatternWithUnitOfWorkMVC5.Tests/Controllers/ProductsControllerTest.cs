using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryPatternWithUnitOfWorkMVC5.Controllers;
using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepositoryPatternWithUnitOfWorkMVC5.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {        
        [TestMethod]
        public void Index_CallService_ReturnViewResultWithListOfProducts()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object, 
                                                    mockCategoryService.Object, 
                                                    mockCategoryProductService.Object);
            
            mockProductService.Setup(x => x.GetAllProducts()).Returns(GetProducts());

            // Act
            var result = controller.Index() as ViewResult;
            var products = result.ViewData.Model as List<Product>;

            // Assert
            Assert.IsNotNull(result);            
            Assert.AreEqual(4, products.Count);
            Assert.AreEqual(1, products[0].Id);
            Assert.AreEqual("Test Product 3", products[2].Name); 
        }

        [TestMethod]
        public void Create_InvalidModel_ReturnsModelWithListOfCategories()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();
            
            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);
            mockCategoryService.Setup(x => x.GetAllCategories()).Returns(GetCategories());

            var productCreateEditViewModel = new ProductCreateEditFormViewModel();
            controller.ModelState.AddModelError("FakeError", "Fake Error Message");
            
            var result = controller.Create(productCreateEditViewModel) as ViewResult;
            var categories = ((ProductCreateEditFormViewModel)result.ViewData.Model).Categories.ToList(); 

            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
            Assert.AreEqual(4, categories.Count);
            Assert.AreEqual(2, categories[1].Id);
            Assert.AreEqual("Accessories", categories[3].Name);
        }

        [TestMethod]
        public void Create_ValidModel_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);
            mockCategoryService.Setup(x => x.GetAllCategories()).Returns(GetCategories());

            var productCreateEditFormViewModel = new ProductCreateEditFormViewModel
            {
                Name = "Test Product",
                Description = "Test Product Description",
                CategoryId = 1
            };

            var result = controller.Create(productCreateEditFormViewModel) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        private List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Test Product 1",Description = "Test Description 1" },
                new Product {Id = 2, Name = "Test Product 2",Description = "Test Description 2" },
                new Product {Id = 3, Name = "Test Product 3",Description = "Test Description 3" },
                new Product {Id = 4, Name = "Test Product 4",Description = "Test Description 4" },
            };
            return products;
        }

        private List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category {Id = 1, Name= "Tops"},
                new Category {Id = 2, Name= "Bottoms"},
                new Category {Id = 3, Name= "Shoes"},
                new Category {Id = 4, Name= "Accessories"},
            };
            return categories;
        }
    }
}
