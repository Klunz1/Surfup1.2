using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfsupEmil.Models;

namespace SurfsupEmil.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly SurfsUpDbContext _context;
        private List<Surfboard> _surfboards = new List<Surfboard>
        {
            new Surfboard { Name = "The Minilog", Length = 6d, Width = 21d, Thickness = 2.75d, Volume = 38.8d, Type = SurfboardType.Shortboard, PriceOfPurchase = 565d, Equipment = null, HourlyPrice = 60},
            new Surfboard { Name = "The Wide Glider", Length = 7.1d, Width = 21.75d, Thickness = 2.75d, Volume = 44.16d, Type = SurfboardType.Funboard, PriceOfPurchase = 685d, Equipment = null, HourlyPrice = 65},
            new Surfboard { Name = "The Golden Ratio", Length = 6.3d, Width = 21.85d, Thickness = 2.9d, Volume = 43.22d, Type = SurfboardType.Funboard, PriceOfPurchase = 695d, Equipment = null, HourlyPrice = 66},
            new Surfboard { Name = "Mahi Mahi", Length = 5.4d, Width = 20.75d, Thickness = 2.3d, Volume = 29.39d, Type = SurfboardType.Fish, PriceOfPurchase = 645d, Equipment = null, HourlyPrice = 60},
            new Surfboard { Name = "The Emerald Glider", Length = 9.2d, Width = 22.8d, Thickness = 2.8d, Volume = 65.4d, Type = SurfboardType.Longboard, PriceOfPurchase = 895d, Equipment = null, HourlyPrice = 90},
            new Surfboard { Name = "The Bomb", Length = 5.5d, Width = 21d, Thickness = 2.5d, Volume = 33.7d, Type = SurfboardType.Shortboard, PriceOfPurchase = 645d, Equipment = null, HourlyPrice = 62},
            new Surfboard { Name = "Walden Magic", Length = 9.6d, Width = 19.4d, Thickness = 3d, Volume = 80d, Type = SurfboardType.Longboard, PriceOfPurchase = 1025d, Equipment = null, HourlyPrice = 92},
            new Surfboard { Name = "Naish One", Length = 12.6d, Width = 30d, Thickness = 6d, Volume = 301d, Type = SurfboardType.SUP, PriceOfPurchase = 854d, Equipment = "Paddle", HourlyPrice = 77},
            new Surfboard { Name = "Six Tourer", Length = 11.6d, Width = 32d, Thickness = 6d, Volume = 270d, Type = SurfboardType.SUP, PriceOfPurchase = 611d, Equipment = "Fin, Paddle, Pump, Leash", HourlyPrice = 55},
            new Surfboard { Name = "Naish Maliko", Length = 14d, Width = 25d, Thickness = 6d, Volume = 330d, Type = SurfboardType.SUP, PriceOfPurchase = 1304d, Equipment = "Fin, Paddle, Pump, Leash", HourlyPrice = 100},
        };

        public SurfboardsController(SurfsUpDbContext context)
        {
            _context = context;
        }

        // GET: Surfboards
        public async Task<IActionResult> Index()
        {
            if (!_context.Surfboards.Any())
            {
                foreach (Surfboard s in _surfboards)
                    _context.Surfboards.Add(s);
            }

            await _context.SaveChangesAsync();

            return View(await _context.Surfboards.ToListAsync());
        }

        // GET: Surfboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboards
                .FirstOrDefaultAsync(m => m.SurfboardId == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // GET: Surfboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboards.FindAsync(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return View(surfboard);
        }

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurfboardId,Name,Length,Width,Thickness,Volume,Type,PriceOfPurchase,Equipment,HourlyPrice, RowVersion")] Surfboard surfboard)
        {
            if (id != surfboard.SurfboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfboardExists(surfboard.SurfboardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(surfboard);
        }

        // GET: Surfboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboards
                .FirstOrDefaultAsync(m => m.SurfboardId == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surfboard = await _context.Surfboards.FindAsync(id);
            if (surfboard != null)
            {
                _context.Surfboards.Remove(surfboard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfboardExists(int id)
        {
            return _context.Surfboards.Any(e => e.SurfboardId == id);
        }
    }
}
