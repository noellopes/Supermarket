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
    public class ReserveDepartmentsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReserveDepartmentsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ReserveDepartments
        public async Task<IActionResult> Index()
        {
              return _context.ReserveDepartment != null ? 
                          View(await _context.ReserveDepartment.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.ReserveDepartment'  is null.");
        }

        // GET: ReserveDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment = await _context.ReserveDepartment
                .FirstOrDefaultAsync(m => m.ReserveDepartmentId == id);
            if (reserveDepartment == null)
            {
                return NotFound();
            }

            return View(reserveDepartment);
        }

        // GET: ReserveDepartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReserveDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveDepartmentId,NumeroDeFunc")] ReserveDepartment reserveDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserveDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserveDepartment);
        }

        // GET: ReserveDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment = await _context.ReserveDepartment.FindAsync(id);
            if (reserveDepartment == null)
            {
                return NotFound();
            }
            return View(reserveDepartment);
        }

        // POST: ReserveDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveDepartmentId,NumeroDeFunc")] ReserveDepartment reserveDepartment)
        {
            if (id != reserveDepartment.ReserveDepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserveDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveDepartmentExists(reserveDepartment.ReserveDepartmentId))
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
            return View(reserveDepartment);
        }

        // GET: ReserveDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment = await _context.ReserveDepartment
                .FirstOrDefaultAsync(m => m.ReserveDepartmentId == id);
            if (reserveDepartment == null)
            {
                return NotFound();
            }

            return View(reserveDepartment);
        }

        // POST: ReserveDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReserveDepartment == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ReserveDepartment'  is null.");
            }
            var reserveDepartment = await _context.ReserveDepartment.FindAsync(id);
            if (reserveDepartment != null)
            {
                _context.ReserveDepartment.Remove(reserveDepartment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveDepartmentExists(int id)
        {
          return (_context.ReserveDepartment?.Any(e => e.ReserveDepartmentId == id)).GetValueOrDefault();
        }
    }
}
