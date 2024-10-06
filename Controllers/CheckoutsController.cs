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
        public async Task<IActionResult> CreateOrder(Order order) // CREATES AN ORDER AND ATTACHES SURFBOARDS AND AN EMAIL TO IT.
        {
            if (CurrentOrder != null)
            {
                await AttachLiveSurfboard(order);
                var userEmail = GetUserEmail();

                if (userEmail != null)
                {
                    order.UserEmail = userEmail;
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(CurrentOrder);
            }
            if (!ValidateOrder(order))
            {
                return View("Index", CurrentOrder);
            }
            return await SaveOrder(order);
        }
        private async Task AttachLiveSurfboard(Order order) // ATTACHES SELECTED SURFBOARDS TO ORDER AND CHECKS FOR CONCURRENCY, I THINK...
        {
            var surfboardId = CurrentOrder.Surfboards.Select(s => s.SurfboardId).ToList();
            var liveSurfboards = await _context.Surfboards
                .Where(s => surfboardId.Contains(s.SurfboardId))
                .ToListAsync();
            foreach (var surfboard in liveSurfboards)
            {
                _context.Entry(surfboard).OriginalValues["RowVersion"] =
                    CurrentOrder.Surfboards.First(x => x.SurfboardId == surfboard.SurfboardId).RowVersion;
                order.AddSurfboard(surfboard);
                _context.Entry(surfboard).State = EntityState.Modified;
            }
        }
        private string GetUserEmail() // SIMPLY RETURNS USEREMAIL. NO NEED FOR FANCY SMANCY METHODS. JUST GRAB IT FROM THE IDENTITY CLASS
        {
            return User.Identity.Name;
        }
        private bool ValidateOrder(Order order)
        {
            if (order.Surfboards.Count <= 0)
            {
                ModelState.AddModelError("Surfboards", "Orderen skal indeholde mindst ét surfboard");
                return false;
            }
            if (order.PickupDate < DateTime.Today)
            {
                ModelState.AddModelError("PickupDate", "Afhentning kan ikke ske i fortiden...");
                return false;
            }
            if (order.ReturnDate < order.PickupDate)
            {
                ModelState.AddModelError("ReturnDate", "Returdato kan ikke være tidligere end afhentningsdato");
                return false;
            }
            if (!order.PickupDate.HasValue || !order.ReturnDate.HasValue)
            {
                ModelState.AddModelError("PickupDate", "Alle felter skal udfyldes");
                return false;
            }
            return ModelState.IsValid;
        }
        private async Task<IActionResult> SaveOrder(Order order) // SAVES ORDER AND CHECKS FOR CONCURRENCY
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                Console.WriteLine("Order was saved.");
                CurrentOrder = new Order();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("Save was not successful because of concurrency issues.");
                ModelState.AddModelError("", "En anden bruger har ændret i det valgte surfboard.");
                return View("Index", CurrentOrder);
            }
            finally
            {
                Console.WriteLine("Save was not successful, concurrency check was also not successful.");
            }
        }
    }











    //    [HttpPost]
    //    public async Task<IActionResult> CreateOrder(Order order)
    //    {
    //        if (CurrentOrder != null)
    //        {
    //            // Hent surfboards fra databasen baseret på deres ID for at undgå at tilføje dem igen
    //            var surfboardIds = CurrentOrder.Surfboards.Select(s => s.SurfboardId).ToList();
    //            var existingSurfboards = await _context.Surfboards
    //                .Where(s => surfboardIds.Contains(s.SurfboardId))
    //                .ToListAsync();

    //            // Tilknyt eksisterende surfboards til ordren
    //            foreach (var surfboard in existingSurfboards)
    //            {
    //                _context.Entry(surfboard).OriginalValues["RowVersion"] = CurrentOrder.Surfboards.Where(x => x.SurfboardId == surfboard.SurfboardId).ToList()[0].RowVersion;
    //                order.AddSurfboard(surfboard);

    //                _context.Entry(surfboard).State = EntityState.Modified;
    //            }
    //            var userEmail = User.Identity.Name;
    //            if (userEmail != null)
    //            {
    //                order.UserEmail = userEmail;
    //                _context.Orders.Add(order);
    //                await _context.SaveChangesAsync();
    //                return RedirectToAction("Index");
    //            }
    //            return View(CurrentOrder);
    //        }

    //        // ERROR HANDLING ADDS MODEL ERRORS PASSED IN THE VIEW.
    //        if (order.Surfboards.Count <= 0)
    //            ModelState.AddModelError("Surfboards", "Orderen skal indeholde mindst ét surfboard");

    //        if (order.PickupDate < DateTime.Today)
    //            ModelState.AddModelError("PickupDate", "Afhentning kan ikke ske i fortiden...");

    //        if (order.ReturnDate < order.PickupDate)
    //            ModelState.AddModelError("ReturnDate", "Returdato kan ikke være tidligere end afhentningsdato");

    //        if (!order.PickupDate.HasValue || !order.ReturnDate.HasValue)
    //            ModelState.AddModelError("PickupDate", "Alle felter skal udfyldes");


    //        if (ModelState.IsValid)
    //        {
    //            try    // Her håndteres concurrency. 
    //            {
    //                await _context.Orders.AddAsync(order);
    //                await _context.SaveChangesAsync();
    //                Console.WriteLine("Save successful!");
    //                CurrentOrder = new Order();
    //                return RedirectToAction("Index", "Home");
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                Console.WriteLine("Save was not successful because of concurrency issues.");
    //                ModelState.AddModelError("", "En anden bruger har ændret i det valgte surfboard. ");
    //                return View("Index", CurrentOrder);
    //            }
    //            finally
    //            {
    //                Console.WriteLine("Save was not successful, concurrency check was also not successful.");
    //            }

    //        }
    //        return View("Index", CurrentOrder);
    //    }
    //}
}
