using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfsupEmil.Models;

namespace SurfsupEmil.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly SurfsUpDbContext _context;
        public static Order CurrentOrder = new Order();
        public CheckoutsController(SurfsUpDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(CurrentOrder);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (CurrentOrder != null)
            {
                // Hent surfboards fra databasen baseret på deres ID for at undgå at tilføje dem igen
                var surfboardIds = CurrentOrder.Surfboards.Select(s => s.SurfboardId).ToList();
                var existingSurfboards = await _context.Surfboards
                    .Where(s => surfboardIds.Contains(s.SurfboardId))
                    .ToListAsync();

                // Tilknyt eksisterende surfboards til ordren
                foreach (var surfboard in existingSurfboards)
                {
                    order.AddSurfboard(surfboard);
                }
            }

            // ERROR HANDLING ADDS MODEL ERRORS PASSED IN THE VIEW.
            if (order.Surfboards.Count <= 0)
                ModelState.AddModelError("Surfboards", "Orderen skal indeholde mindst ét surfboard");

            if (order.PickupDate < DateTime.Today)
                ModelState.AddModelError("PickupDate", "Afhentning kan ikke ske i fortiden...");

            if (order.ReturnDate < order.PickupDate)
                ModelState.AddModelError("ReturnDate", "Returdato kan ikke være tidligere end afhentningsdato");

            if (!order.PickupDate.HasValue || !order.ReturnDate.HasValue)
                ModelState.AddModelError("PickupDate", "Alle felter skal udfyldes");


            if (ModelState.IsValid)
            {
                await _context.Orders.AddAsync(order);


                try    // Her håndteres concurrency. 
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Save successful!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Save was not successful because of concurrency issues.");
                }
                finally
                {
                    Console.WriteLine("Save was not successful, concurrency check was also not successful.");
                }

                CurrentOrder = new Order();
                return RedirectToAction("Index", "Home");
            }
            return View("Index", CurrentOrder);
        }
    }
}
