using Microsoft.AspNetCore.Mvc;

namespace Kilimo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
