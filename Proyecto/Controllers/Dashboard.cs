using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
