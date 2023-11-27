using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class HallwaysController : Controller
    {
        private readonly SupermarketDbContext _context;

        public HallwaysController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Hallways
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Hallway.Include(h => h.Store);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Hallways/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway
                .Include(h => h.Store)
                .FirstOrDefaultAsync(m => m.HallwayId == id);
            if (hallway == null)
            {
                return NotFound();
            }

            return View(hallway);
        }

        // GET: Hallways/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name");
            return View();
        }

        // POST: Hallways/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HallwayId,Description,StoreId")] Hallway hallway)
        {
            if (ModelState.IsValid)
            {
                bool HallwaysExists = await _context.Hallway.AnyAsync(
                b => b.Description == hallway.Description && b.StoreId == hallway.StoreId);

                if (HallwaysExists)
                {
                    ModelState.AddModelError("", "Another hallway with the same description and store already exists.");
                }
                else{
                    _context.Add(hallway);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "Hallways successfully created.";
                    hallway.Store = await _context.Store.FindAsync(hallway.StoreId);
                    return View("Details", hallway);
                }
            }
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return View(hallway);
        }

        // GET: Hallways/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway.FindAsync(id);
            if (hallway == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return View(hallway);
        }

        // POST: Hallways/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HallwayId,Description,StoreId")] Hallway hallway)
        {
            if (id != hallway.HallwayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    bool HallwaysExists = await _context.Hallway.AnyAsync(
                    b => b.Description == hallway.Description && b.StoreId == hallway.StoreId && b.HallwayId!=hallway.HallwayId);

                    if (HallwaysExists)
                    {
                        ModelState.AddModelError("", "Another Hallways with the same Description and Store already exists.");
                    }
                    else{
                        _context.Update(hallway);
                        await _context.SaveChangesAsync();

                        ViewBag.Message = "Hallways successfully edited.";
                       
                        hallway.Store = await _context.Store.FindAsync(hallway.StoreId);
                        return View("Details", hallway);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallwayExists(hallway.HallwayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return View(hallway);
        }

        // GET: Hallways/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway
                .Include(h => h.Store)
                .FirstOrDefaultAsync(m => m.HallwayId == id);
            if (hallway == null)
            {
                return NotFound();
            }
            var ShelfsAssociatedWithHallway = await _context.Shelf
            .Where(s => s.HallwayId == id)
            .ToListAsync();

            if (ShelfsAssociatedWithHallway.Count > 0)
            {
                ViewBag.ErrorMessage = "It is not possible to delete the hallway as there are shelves associated with it";
                ViewBag.ShelfsAssociatedWithHallway = ShelfsAssociatedWithHallway;
                return View("Delete");
            }

            return View(hallway);
        }

        // POST: Hallways/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hallway == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Hallway'  is null.");
            }
            var hallway = await _context.Hallway.FindAsync(id);
            if (hallway != null)
            {
                _context.Hallway.Remove(hallway);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallwayExists(int id)
        {
          return (_context.Hallway?.Any(e => e.HallwayId == id)).GetValueOrDefault();
        }
    }
}
