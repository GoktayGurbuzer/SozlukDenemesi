using Microsoft.AspNetCore.Mvc;

namespace Sozluk42.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
