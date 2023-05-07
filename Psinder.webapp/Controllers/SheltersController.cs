using Microsoft.AspNetCore.Mvc;

namespace Psinder.webapp.Controllers
{
    public class SheltersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
