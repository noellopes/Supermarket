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
    public class ReserveDepartments1Controller : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReserveDepartments1Controller(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ReserveDepartments1
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ReserveDepartment.Include(r => r.Employee).Include(r => r.Reserve);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ReserveDepartments1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment = await _context.ReserveDepartment
                .Include(r => r.Employee)
                .Include(r => r.Reserve)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (reserveDepartment == null)
            {
                return NotFound();
            }

            return View(reserveDepartment);
        }

        // GET: ReserveDepartments1/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId");
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View();
        }

        // POST: ReserveDepartments1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveId,EmployeeId,NumeroDeFunc")] ReserveDepartment reserveDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserveDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment.ReserveId);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc", reserveDepartment.NumeroDeFunc);
            return View(reserveDepartment);
        }

        // GET: ReserveDepartments1/Edit/5
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment.ReserveId);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View(reserveDepartment);
        }

        // POST: ReserveDepartments1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,Employee_Name,NumeroDeFunc")] ReserveDepartment reserveDepartment)
        {
            if (id != reserveDepartment.ReserveId)
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
                    if (!ReserveDepartmentExists(reserveDepartment.ReserveId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment.ReserveId);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");

            return View(reserveDepartment);
        }

        // GET: ReserveDepartments1/Delete/5
        public async Task<IActionResult> Delete(int? id, int? id2)
        {
            if (id == null || id2 == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment = await _context.ReserveDepartment
                .Include(r => r.Employee)
                .Include(r => r.Reserve)
                .FirstOrDefaultAsync(m => m.ReserveId == id && m.EmployeeId == id2);

            if (reserveDepartment == null)
            {
                return NotFound();
            }

            return View(reserveDepartment);
        }

        // POST: ReserveDepartments1/Delete/5
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
          return (_context.ReserveDepartment?.Any(e => e.ReserveId == id)).GetValueOrDefault();
        }
    }
}
