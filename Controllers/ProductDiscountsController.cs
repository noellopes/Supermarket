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
    public class ProductDiscountsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ProductDiscountsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ProductDiscounts
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ProductDiscount.Include(p => p.Product);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ProductDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductDiscountId == id);
            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // GET: ProductDiscounts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description");
            return View();
        }

        // POST: ProductDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductDiscountId,ProductId,Value,StartDate,EndDate")] ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productDiscount.ProductId);
            return View(productDiscount);
        }

        // POST: ProductDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductDiscountId,ProductId,Value,StartDate,EndDate")] ProductDiscount productDiscount)
        {
            if (id != productDiscount.ProductDiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDiscountExists(productDiscount.ProductDiscountId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductDiscountId == id);
            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // POST: ProductDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductDiscount == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ProductDiscount'  is null.");
            }
            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount != null)
            {
                _context.ProductDiscount.Remove(productDiscount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDiscountExists(int id)
        {
          return (_context.ProductDiscount?.Any(e => e.ProductDiscountId == id)).GetValueOrDefault();
        }
    }
}
