using RepositoryPatternWithUnitOfWorkMVC5.Models;
using RepositoryPatternWithUnitOfWorkMVC5.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPatternWithUnitOfWorkMVC5.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public ActionResult Index()
        {            
            return View(_categoryService.GetAllCategories());
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            _categoryService.AddCategory(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {            
            return View(_categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (!ModelState.IsValid)            
                return View(model);
                

            _categoryService.EditCategory(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public ActionResult Delete(Category model)
        {
            if (_categoryService.IsCategoryExists(model.Id))
            {
                _categoryService.RemoveCategory(model);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(_categoryService.GetCategoryById(id));
        }

        public ActionResult GetAllCategoriesWithProducts()
        {
            return View(_categoryService.GetAllCategoriesWithProducts());
        }

    }
}