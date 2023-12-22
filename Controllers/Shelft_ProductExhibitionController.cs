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
        public IActionResult Create(int? shelfId)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            ViewBag.ShelfId2 = shelfId;
            ViewBag.ShelfIdName = _context.Shelf.Find(shelfId.Value)?.Name;
            ViewBag.ProductIdOptions = new SelectList(_context.Product, "ProductId", "Name");

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

                    TempData["ErrorMessage"] = "Another Shelft Product Exhibition Exists with the same Product and Shelf already exists.";
                }
                else
                {
                    try
                    {
                        _context.Add(shelft_ProductExhibition);
                        await _context.SaveChangesAsync();

                        // Load references
                        await _context.Entry(shelft_ProductExhibition)
                            .Reference(wp => wp.Product)
                            .LoadAsync();
                        await _context.Entry(shelft_ProductExhibition)
                            .Reference(wp => wp.Shelf)
                            .LoadAsync();

                        ViewBag.Message = "Shelft Product Exhibition successfully created.";

                        // Redirect to the "Details" action with the associated shelf ID
                        return RedirectToAction("Details", "Shelves", new { id = shelft_ProductExhibition.ShelfId });
                    }
                    catch (DbUpdateException)
                    {
                        // Handle any exceptions that might occur during saving changes
                        // You can log the exception or show an error message to the user
                        ModelState.AddModelError("", "An error occurred while saving the data.");
                    }
                }
            }

            // If ModelState is not valid or there was an exception, return to the "Create" view with errors and the same shelf ID
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", shelft_ProductExhibition.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", shelft_ProductExhibition.ShelfId);

        
            return RedirectToAction("Create", "shelft_ProductExhibition", new { shelfId = shelft_ProductExhibition.ShelfId });
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

                    //ViewBag.Message = "Shelft Product Exhibition successfully edited.";
                    TempData["Message"] = "Shelft Product Exhibition successfully edited";
                    return RedirectToAction("Details", new { productId = shelft_ProductExhibition.ProductId, shelfId = shelft_ProductExhibition.ShelfId });
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

            if (shelft_ProductExhibition.Quantity != 0)
            {
                ViewBag.Message = "Shelft Product Exhibition cannot be eliminated because it has a quantity greater than 0";
                return View("Delete", shelft_ProductExhibition); 
            }
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
