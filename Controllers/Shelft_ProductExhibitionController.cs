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
    public class Shelft_ProductExhibitionController : Controller
    {
        private readonly SupermarketDbContext _context;

        public Shelft_ProductExhibitionController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Shelft_ProductExhibition
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Shelft_ProductExhibition.Include(s => s.Product).Include(s => s.Shelf);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Shelft_ProductExhibition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition
                .Include(s => s.Product)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description");
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name");
            return View();
        }

        // POST: Shelft_ProductExhibition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ShelfId,Quantity,MinimumQuantity")] Shelft_ProductExhibition shelft_ProductExhibition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shelft_ProductExhibition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition.FindAsync(id);
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // POST: Shelft_ProductExhibition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ShelfId,Quantity,MinimumQuantity")] Shelft_ProductExhibition shelft_ProductExhibition)
        {
            if (id != shelft_ProductExhibition.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelft_ProductExhibition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Shelft_ProductExhibitionExists(shelft_ProductExhibition.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition
                .Include(s => s.Product)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            return View(shelft_ProductExhibition);
        }

        // POST: Shelft_ProductExhibition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shelft_ProductExhibition == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Shelft_ProductExhibition'  is null.");
            }
            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition.FindAsync(id);
            if (shelft_ProductExhibition != null)
            {
                _context.Shelft_ProductExhibition.Remove(shelft_ProductExhibition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Shelft_ProductExhibitionExists(int id)
        {
          return (_context.Shelft_ProductExhibition?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
