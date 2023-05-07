using Microsoft.AspNetCore.Mvc;

namespace Psinder.webapp.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
