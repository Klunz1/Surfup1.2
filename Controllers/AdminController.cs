using Microsoft.AspNetCore.Mvc;

namespace SurfsupEmil.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GoToSurfboards()
        {
            return RedirectToAction("Index", "Surfboards");
        }
        public IActionResult GoToOrders()
        {
            return RedirectToAction("Index", "Orders");
        }
    }
}
