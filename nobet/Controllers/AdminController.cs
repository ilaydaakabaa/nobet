using Microsoft.AspNetCore.Mvc;

namespace nobet.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View(); // AdminDashboard.cshtml sayfasını döndür
        }
    }
}
