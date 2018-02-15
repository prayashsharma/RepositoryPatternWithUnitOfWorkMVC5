using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using RepositoryPatternWithUnitOfWorkMVC5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPatternWithUnitOfWorkMVC5.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAndProductService _categoryAndProductService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ICategoryAndProductService categoryAndProductService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _categoryAndProductService = categoryAndProductService ?? throw new ArgumentNullException(nameof(categoryAndProductService));
        }
        public ActionResult Index()
        {
            var products = _productService.GetAllProducts();            
            return View(products);
        }

        public ActionResult Create()
        {
            var model = new ProductCreateEditFormViewModel()
            {
                Categories = _categoryService.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductCreateEditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetAllCategories();
                return View(model);
            };

            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId
            };

            _productService.AddProduct(product);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(Product product)
        {
            if (_productService.IsProductExists(product.Id))
            {
                _productService.RemoveProduct(product);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            var model = new ProductCreateEditFormViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Categories = _categoryService.GetAllCategories()
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductCreateEditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetAllCategories();
                return View(model);
            }

            var product = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId
            };

            _productService.EditProduct(product);
            return RedirectToAction("Index");
        }

        public ActionResult FindByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return RedirectToAction("Index");

            var product = _productService.GetProductByName(name);
            if (product == null)
                return HttpNotFound();

            //just for the demo, I am returning single product as a list
            return View("Index", new List<Product> { product });
        }

        public ActionResult GetAllWithCategory()
        {
            var products = _productService.GetAllProductsWithCategory();
            if (products == null)
                return HttpNotFound();

            return View("Index", products);
        }

        public ActionResult GetOneWithCategory(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var product = _productService.GetProductWithCategory(id.GetValueOrDefault());
            if (product == null)
                return HttpNotFound();

            //just for the demo, I am returning single product as a list
            return View("Index", new List<Product> { product });

        }

        public ActionResult FindById(int? id)
        {            
            if (id == null)            
                return RedirectToAction("Index");            

            var product = _productService.GetProductById(id.GetValueOrDefault());
            if (product == null)
                return HttpNotFound();


            //just for the demo, I am returning single product as a list
            return View("Index", new List<Product> { product });
        }

        // Update related entities at the same time.
        public ActionResult CreateCategoryAndProducts()
        {
            var newCategory = new Category
            {
                Name = "TestCategory"
            };

            var newProducts = new List<Product>
            {
                new Product {Name = "P1",Description = "blah blah 1" },
                new Product {Name = "P2",Description = "blah blah 2" },
                new Product {Name = "P3",Description = "blah blah 3" },
                new Product {Name = "P4",Description = "blah blah 4" },
            };            
            _categoryAndProductService.AddCategoryWithProduct(newCategory, newProducts);
            return RedirectToAction("Index");
        }
    }
}