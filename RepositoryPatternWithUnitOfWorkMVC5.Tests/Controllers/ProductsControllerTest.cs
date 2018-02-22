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
        public void Create_PostInvalidModel_ReturnsModelWithListOfCategories()
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
        public void Create_PostValidModel_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);           

            var productCreateEditFormViewModel = new ProductCreateEditFormViewModel
            {
                Name = "Test Product",
                Description = "Test Product Description",
                CategoryId = 1
            };

            var result = controller.Create(productCreateEditFormViewModel) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void Create_GetRequest_ReturnViewResult()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockCategoryService.Setup(x => x.GetAllCategories()).Returns(GetCategories());

            var result = controller.Create();
            var vm = result as ViewResult;
            var categories = ((ProductCreateEditFormViewModel)vm.ViewData.Model).Categories.ToList();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(4, categories.Count);
            Assert.AreEqual(3, categories[2].Id);
        }

        [TestMethod]
        public void Edit_PostInvalidModel_ReturnsModelWithListOfCategories()
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

            var result = controller.Edit(productCreateEditViewModel) as ViewResult;
            var categories = ((ProductCreateEditFormViewModel)result.ViewData.Model).Categories.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
            Assert.AreEqual(4, categories.Count);
            Assert.AreEqual(2, categories[1].Id);
            Assert.AreEqual("Accessories", categories[3].Name);
        }

        [TestMethod]
        public void Edit_PostValidModel_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);            

            var productCreateEditFormViewModel = new ProductCreateEditFormViewModel
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Product Description",
                CategoryId = 1
            };

            var result = controller.Edit(productCreateEditFormViewModel) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void Edit_GetRequest_ReturnViewResultWithProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductById(3)).Returns(GetProducts().Single(x => x.Id == 3));
            mockCategoryService.Setup(x => x.GetAllCategories()).Returns(GetCategories());

            var result = controller.Edit(3) as ViewResult;
            var productCreateEditFormViewModel = result.ViewData.Model as ProductCreateEditFormViewModel;            

            Assert.IsNotNull(productCreateEditFormViewModel);
            Assert.AreEqual(3, productCreateEditFormViewModel.Id);
            Assert.AreEqual("Test Product 3", productCreateEditFormViewModel.Name);
            Assert.AreEqual("Test Description 3", productCreateEditFormViewModel.Description);
            Assert.AreEqual(4, productCreateEditFormViewModel.CategoryId);
            Assert.AreEqual(4, productCreateEditFormViewModel.Categories.ToList().Count);
        }

        [TestMethod]
        public void Delete_GetRequest_ReturnViewResultWithProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductById(3)).Returns(GetProducts().Single(x => x.Id == 3));
            mockCategoryService.Setup(x => x.GetAllCategories()).Returns(GetCategories());

            var result = controller.Delete(3) as ViewResult;
            var product = result.ViewData.Model as Product;

            Assert.IsNotNull(product);
            Assert.AreEqual(3, product.Id);
            Assert.AreEqual("Test Product 3", product.Name);
            Assert.AreEqual("Test Description 3", product.Description);
            Assert.AreEqual(4, product.CategoryId);           
        }

        [TestMethod]
        public void Delete_PostValidModel_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            var product = GetProducts().Single(x => x.Id == 2);
            var result = controller.Delete(product) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void FindByName_NameIsNull_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            var result = controller.FindByName(null) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void FindByName_NameNotNull_ReturnsViewResultWithProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);
            mockProductService.Setup(x => x.GetProductByName("Test Product 2"))
                .Returns(GetProducts().Single(x => x.Name == "Test Product 2"));

            var result = controller.FindByName("Test Product 2") as ViewResult;
            var products = result.ViewData.Model as List<Product>;

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products[0].Id);
            Assert.AreEqual("Test Product 2", products[0].Name);
            Assert.AreEqual("Test Description 2", products[0].Description);
            Assert.AreEqual(2, products[0].CategoryId);
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void FindByName_ProductIsNull_ReturnHttpNotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductByName("Test Product 2")).Returns(default(Product));

            var result = controller.FindByName("Test Product 2");
            var model = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.IsNull(model);

        }

        [TestMethod]
        public void GetAllWithCategory_ProductsNotFound_ReturnHttpNotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetAllProductsWithCategory()).Returns(default(List<Product>));            

            var result = controller.GetAllWithCategory();
            var model = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.IsNull(model);
        }

        [TestMethod]
        public void GetAllWithCategory_ProductsFound_ReturnsViewResultWithProducts()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);
            mockProductService.Setup(x => x.GetAllProductsWithCategory()).Returns(GetProducts());

            var result = controller.GetAllWithCategory() as ViewResult;            
            var products = result.ViewData.Model as List<Product>;

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products[1].Id);
            Assert.AreEqual("Test Product 3", products[2].Name);
            Assert.AreEqual("Test Description 1", products[0].Description);
            Assert.AreEqual(4, products[2].CategoryId);
            Assert.AreEqual(4, products.Count);
            Assert.AreEqual("Index", result.ViewName);
            
        }


        [TestMethod]
        public void GetOneWithCategory_IdIsNull_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            var result = controller.GetOneWithCategory(null) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void GetOneWithCategory_IdNotNull_ReturnsViewResultWithProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductWithCategory(1))
                .Returns(GetProducts().Single(x => x.Id == 1));

            var result = controller.GetOneWithCategory(1) as ViewResult;
            var products = result.ViewData.Model as List<Product>;

            Assert.IsNotNull(products);
            Assert.AreEqual(1, products[0].Id);
            Assert.AreEqual("Test Product 1", products[0].Name);
            Assert.AreEqual("Test Description 1", products[0].Description);
            Assert.AreEqual(2, products[0].CategoryId);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void GetOneWithCategory_ProductIsNull_ReturnHttpNotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductWithCategory(1)).Returns(default(Product));

            var result = controller.GetOneWithCategory(1);
            var model = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.IsNull(model);

        }

        [TestMethod]
        public void FindById_IdIsNull_RedirectsToActionIndex()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            var result = controller.FindById(null) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Index");
        }

        [TestMethod]
        public void FindById_IdNotNull_ReturnsViewResultWithProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductById(1))
                .Returns(GetProducts().Single(x => x.Id == 1));

            var result = controller.FindById(1) as ViewResult;
            var products = result.ViewData.Model as List<Product>;

            Assert.IsNotNull(products);
            Assert.AreEqual(1, products[0].Id);
            Assert.AreEqual("Test Product 1", products[0].Name);
            Assert.AreEqual("Test Description 1", products[0].Description);
            Assert.AreEqual(2, products[0].CategoryId);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void FindById_ProductIsNull_ReturnHttpNotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockCategoryProductService = new Mock<ICategoryAndProductService>();

            var controller = new ProductsController(mockProductService.Object,
                                                                mockCategoryService.Object,
                                                                mockCategoryProductService.Object);

            mockProductService.Setup(x => x.GetProductById(1)).Returns(default(Product));

            var result = controller.FindById(1);
            var model = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.IsNull(model);
        }

        private List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Test Product 1",Description = "Test Description 1", CategoryId = 2},
                new Product {Id = 2, Name = "Test Product 2",Description = "Test Description 2", CategoryId = 2} ,
                new Product {Id = 3, Name = "Test Product 3",Description = "Test Description 3", CategoryId = 4},
                new Product {Id = 4, Name = "Test Product 4",Description = "Test Description 4", CategoryId = 2},
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
