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
    public class DepartmentsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public DepartmentsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Departments.Include(d => d.Schedule);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Schedule)
                .FirstOrDefaultAsync(m => m.Id_Departments == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ScheduleID", "ScheduleID");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Departments,Name_Departments,Description_Departments,ScheduleID")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ScheduleID", "ScheduleID", departments.ScheduleID);
            return View(departments);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ScheduleID", "ScheduleID", departments.ScheduleID);
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Departments,Name_Departments,Description_Departments,ScheduleID")] Departments departments)
        {
            if (id != departments.Id_Departments)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.Id_Departments))
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
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ScheduleID", "ScheduleID", departments.ScheduleID);
            return View(departments);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Schedule)
                .FirstOrDefaultAsync(m => m.Id_Departments == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Departments'  is null.");
            }
            var departments = await _context.Departments.FindAsync(id);
            if (departments != null)
            {
                _context.Departments.Remove(departments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
          return (_context.Departments?.Any(e => e.Id_Departments == id)).GetValueOrDefault();
        }
    }
}
