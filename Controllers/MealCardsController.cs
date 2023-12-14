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
    public class MealCardsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public MealCardsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: MealCards
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.MealCard.Include(m => m.Employee);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: MealCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MealCard == null)
            {
                return NotFound();
            }

            var mealCard = await _context.MealCard
                .Include(m => m.Employee)
                .Include(m => m.CardMovements)
                .FirstOrDefaultAsync(m => m.MealCardId == id);
            if (mealCard == null)
            {
                return NotFound();
            }
            
            return View(mealCard);
        }

        // GET: MealCards/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: MealCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealCardId,Balance,EmployeeId")] MealCard mealCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", mealCard.EmployeeId);
            return View(mealCard);
        }

        // GET: MealCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MealCard == null)
            {
                return NotFound();
            }

            var mealCard = await _context.MealCard.FindAsync(id);
            if (mealCard == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", mealCard.EmployeeId);
            return View(mealCard);
        }

        // POST: MealCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealCardId,Balance,EmployeeId")] MealCard mealCard)
        {
            if (id != mealCard.MealCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealCardExists(mealCard.MealCardId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", mealCard.EmployeeId);
            return View(mealCard);
        }

        // GET: MealCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MealCard == null)
            {
                return NotFound();
            }

            var mealCard = await _context.MealCard
                .Include(m => m.Employee)
                .FirstOrDefaultAsync(m => m.MealCardId == id);
            if (mealCard == null)
            {
                return NotFound();
            }

            return View(mealCard);
        }

        // POST: MealCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MealCard == null)
            {
                return Problem("Entity set 'SupermarketDbContext.MealCard'  is null.");
            }
            var mealCard = await _context.MealCard.FindAsync(id);
            if (mealCard != null)
            {
                _context.MealCard.Remove(mealCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealCardExists(int id)
        {
          return (_context.MealCard?.Any(e => e.MealCardId == id)).GetValueOrDefault();
        }
    }
}
