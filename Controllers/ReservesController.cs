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
    public class ReservesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReservesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Reserves
        public async Task<IActionResult> Index()
        {
              return _context.Reserve != null ? 
                          View(await _context.Reserve.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Reserve'  is null.");
        }

        // GET: Reserves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserve == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserve
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // GET: Reserves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveId")] Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reserve == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserve.FindAsync(id);
            if (reserve == null)
            {
                return NotFound();
            }
            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId")] Reserve reserve)
        {
            if (id != reserve.ReserveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExists(reserve.ReserveId))
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
            return View(reserve);
        }

        // GET: Reserves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserve == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserve
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserve == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Reserve'  is null.");
            }
            var reserve = await _context.Reserve.FindAsync(id);
            if (reserve != null)
            {
                _context.Reserve.Remove(reserve);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExists(int id)
        {
          return (_context.Reserve?.Any(e => e.ReserveId == id)).GetValueOrDefault();
        }
    }
}
