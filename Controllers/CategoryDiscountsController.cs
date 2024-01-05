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
            var supermarketDbContext = _context.CategoryDiscounts.Include(c => c.Category);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: CategoryDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscounts
                .Include(c => c.Category)
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: CategoryDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryDiscountId,CategoryId,Value,StartDate,EndDate")] CategoryDiscount categoryDiscount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscount.CategoryId);
            return View(categoryDiscount);
        }

        // GET: CategoryDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscounts.FindAsync(id);
            if (categoryDiscount == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscount.CategoryId);
            return View(categoryDiscount);
        }

        // POST: CategoryDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryDiscountId,CategoryId,Value,StartDate,EndDate")] CategoryDiscount categoryDiscount)
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscount.CategoryId);
            return View(categoryDiscount);
        }

        // GET: CategoryDiscounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscount = await _context.CategoryDiscounts
                .Include(c => c.Category)
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
            if (_context.CategoryDiscounts == null)
            {
                return Problem("Entity set 'SupermarketDbContext.CategoryDiscounts'  is null.");
            }
            var categoryDiscount = await _context.CategoryDiscounts.FindAsync(id);
            if (categoryDiscount != null)
            {
                _context.CategoryDiscounts.Remove(categoryDiscount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDiscountExists(int id)
        {
          return (_context.CategoryDiscounts?.Any(e => e.CategoryDiscountId == id)).GetValueOrDefault();
        }
    }
}
