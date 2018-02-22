using System.Web.Mvc;

namespace RepositoryPatternWithUnitOfWorkMVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}