using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
        public async Task<IActionResult> Details(int? productId, int? shelfId)
        {
            if (productId == null || shelfId == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition
                .Include(s => s.Product)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(m => m.ProductId == productId && m.ShelfId == shelfId); ;
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Shelf, "ProductId", "Name");
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
                bool Shelft_ProductExhibitionExists = await _context.Shelft_ProductExhibition.AnyAsync(
               s => s.ProductId == shelft_ProductExhibition.ProductId
               && s.ShelfId == shelft_ProductExhibition.ShelfId);
                if (Shelft_ProductExhibitionExists)
                {
                    ModelState.AddModelError("", "Another Shelft Product Exhibition Exists with the same Product and Shelf already exists.");
                }
                else
                {
                    _context.Add(shelft_ProductExhibition);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Shelft Product Exhibition successfully created.";
                    return View("Details", shelft_ProductExhibition);
                }
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Edit/5
        public async Task<IActionResult> Edit(int? productId, int? shelfId)
        {
            if (productId == null || shelfId == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }
            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition
               .Include(s => s.Product)
               .Include(s => s.Shelf)
               .FirstOrDefaultAsync(m => m.ProductId == productId && m.ShelfId == shelfId);

    
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // POST: Shelft_ProductExhibition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int productId, int shelfId, [Bind("ProductId,ShelfId,Quantity,MinimumQuantity")] Shelft_ProductExhibition shelft_ProductExhibition)
        {
            if (productId == null || shelfId == null || shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            if (productId != shelft_ProductExhibition.ProductId|| shelfId!= shelft_ProductExhibition.ShelfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelft_ProductExhibition);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "Shelft Product Exhibition successfully edited.";
                    return View("Details", shelft_ProductExhibition);
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
               // return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);
            return View(shelft_ProductExhibition);
        }

        // GET: Shelft_ProductExhibition/Delete/5
        public async Task<IActionResult> Delete(int? productId, int? shelfId)
        {
            if (productId == null || shelfId == null || _context.Shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition
                .Include(s => s.Product)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(m => m.ProductId == productId && m.ShelfId == shelfId);
            if (shelft_ProductExhibition == null)
            {
                return NotFound();
            }

            return View(shelft_ProductExhibition);
        }

        // POST: Shelft_ProductExhibition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int productId, int shelfId)
        {
            if (_context.Shelft_ProductExhibition == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Shelft_ProductExhibition'  is null.");
            }
            var shelft_ProductExhibition = await _context.Shelft_ProductExhibition.
                FirstOrDefaultAsync(m => m.ProductId == productId && m.ShelfId == shelfId);
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
