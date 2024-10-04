using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsupEmil.Models;

namespace SurfsupEmil.Controllers
{
    public class CartsController : Controller
    {
        private readonly SurfsUpDbContext _context;

        public CartsController(SurfsUpDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            // Find det valgte surfboard baseret på ID
            var surfboard = await _context.Surfboards
                .FirstOrDefaultAsync(m => m.SurfboardId == id);
            if (surfboard is Surfboard && surfboard != null)
                _context.Entry(surfboard).OriginalValues["RowVersion"] = surfboard.RowVersion;

            // Tilføj surfboard til ordren
            CheckoutsController.CurrentOrder.AddSurfboard(surfboard);


            // Eventuelt redirect til kurvsiden eller opdater den aktuelle side
            return RedirectToAction("Index");
        }
    }
}
