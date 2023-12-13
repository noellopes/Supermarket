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
    public class CategoryDiscountsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public CategoryDiscountsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: CategoryDiscounts
        public async Task<IActionResult> Index()
        {
              return _context.CategoryDiscount != null ? 
                          View(await _context.CategoryDiscount.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.CategoryDiscount'  is null.");
        }

        // GET: CategoryDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryDiscount == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscount
                .FirstOrDefaultAsync(m => m.CategoryDiscountId == id);
            if (categoryDiscount == null)
            {
                return NotFound();
            }

            return View(categoryDiscount);
        }

        // GET: CategoryDiscounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryDiscountId,Value,startDate,endDate")] CategoryDiscount categoryDiscount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDiscount);
        }

        // GET: CategoryDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryDiscount == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscount.FindAsync(id);
            if (categoryDiscount == null)
            {
                return NotFound();
            }
            return View(categoryDiscount);
        }

        // POST: CategoryDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryDiscountId,Value,startDate,endDate")] CategoryDiscount categoryDiscount)
        {
            if (id != categoryDiscount.CategoryDiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryDiscountExists(categoryDiscount.CategoryDiscountId))
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
            return View(categoryDiscount);
        }

        // GET: CategoryDiscounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryDiscount == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscount
                .FirstOrDefaultAsync(m => m.CategoryDiscountId == id);
            if (categoryDiscount == null)
            {
                return NotFound();
            }

            return View(categoryDiscount);
        }

        // POST: CategoryDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryDiscount == null)
            {
                return Problem("Entity set 'SupermarketDbContext.CategoryDiscount'  is null.");
            }
            var categoryDiscount = await _context.CategoryDiscount.FindAsync(id);
            if (categoryDiscount != null)
            {
                _context.CategoryDiscount.Remove(categoryDiscount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDiscountExists(int id)
        {
          return (_context.CategoryDiscount?.Any(e => e.CategoryDiscountId == id)).GetValueOrDefault();
        }
    }
}
