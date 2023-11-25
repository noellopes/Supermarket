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
    public class ShelvesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ShelvesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Shelf.Include(s => s.Hallway);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf
                .Include(s => s.Hallway)
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // GET: Shelves/Create
        public IActionResult Create()
        {
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description");
            return View();
        }

        // POST: Shelves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShelfId,Name,HallwayId")] Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                bool ShelvesExists = await _context.Shelf.AnyAsync(
               b => b.Name == shelf.Name && b.HallwayId == shelf.HallwayId);

                if (ShelvesExists)
                {
                    ModelState.AddModelError("", "Another Shelf with the same Name and Hallway already exists.");
                }
                else
                {
                    _context.Add(shelf);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "Shelf successfully created.";
                    shelf.Hallway = await _context.Hallway.FindAsync(shelf.HallwayId);
                    return View("Details", shelf);
                }
            }
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }

        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShelfId,Name,HallwayId")] Shelf shelf)
        {
            if (id != shelf.ShelfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ShelvesExists = await _context.Shelf.AnyAsync(
                    b => b.Name == shelf.Name && b.HallwayId == shelf.HallwayId);



                    if (ShelvesExists)
                    {
                        ModelState.AddModelError("", "Another Shelf with the same Name and Hallway already exists.");
                    }
                    else
                    {
                        _context.Update(shelf);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Hallways successfully edited.";
                        shelf.Hallway = await _context.Hallway.FindAsync(shelf.HallwayId);

                        return View("Details", shelf);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelfExists(shelf.ShelfId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
              //  return RedirectToAction(nameof(Index));
            }
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf
                .Include(s => s.Hallway)
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shelf == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Shelf'  is null.");
            }
            var shelf = await _context.Shelf.FindAsync(id);
            if (shelf != null)
            {
                _context.Shelf.Remove(shelf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelfExists(int id)
        {
          return (_context.Shelf?.Any(e => e.ShelfId == id)).GetValueOrDefault();
        }
    }
}
