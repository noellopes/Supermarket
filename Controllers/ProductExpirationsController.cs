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
    public class ProductExpirationsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ProductExpirationsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ProductExpirations
        public async Task<IActionResult> Index()
        {
              return _context.ProductExpiration != null ? 
                          View(await _context.ProductExpiration.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.ProductExpiration'  is null.");
        }

        // GET: ProductExpirations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductExpiration == null)
            {
                return NotFound();
            }

            var productExpiration = await _context.ProductExpiration
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (productExpiration == null)
            {
                return NotFound();
            }

            return View(productExpiration);
        }

        // GET: ProductExpirations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductExpirations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchId,BatchNumber,ExpirationDate,Quantity")] ProductExpiration productExpiration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productExpiration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productExpiration);
        }

        // GET: ProductExpirations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductExpiration == null)
            {
                return NotFound();
            }

            var productExpiration = await _context.ProductExpiration.FindAsync(id);
            if (productExpiration == null)
            {
                return NotFound();
            }
            return View(productExpiration);
        }

        // POST: ProductExpirations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchId,BatchNumber,ExpirationDate,Quantity")] ProductExpiration productExpiration)
        {
            if (id != productExpiration.BatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productExpiration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExpirationExists(productExpiration.BatchId))
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
            return View(productExpiration);
        }

        // GET: ProductExpirations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductExpiration == null)
            {
                return NotFound();
            }

            var productExpiration = await _context.ProductExpiration
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (productExpiration == null)
            {
                return NotFound();
            }

            return View(productExpiration);
        }

        // POST: ProductExpirations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductExpiration == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ProductExpiration'  is null.");
            }
            var productExpiration = await _context.ProductExpiration.FindAsync(id);
            if (productExpiration != null)
            {
                _context.ProductExpiration.Remove(productExpiration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExpirationExists(int id)
        {
          return (_context.ProductExpiration?.Any(e => e.BatchId == id)).GetValueOrDefault();
        }
    }
}
