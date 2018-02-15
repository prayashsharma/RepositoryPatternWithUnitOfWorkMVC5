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
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {            
            return View(_categoryService.GetAllCategories());
        }
    }
}