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
    public class FormationEmployeesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FormationEmployeesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: FormationEmployees
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.FormationEmployee.Include(f => f.Employee).Include(f => f.Formation);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: FormationEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormationEmployee == null)
            {
                return NotFound();
            }

            var formationEmployee = await _context.FormationEmployee
                .Include(f => f.Employee)
                .Include(f => f.Formation)
                .FirstOrDefaultAsync(m => m.FormationEmployeeId == id);
            if (formationEmployee == null)
            {
                return NotFound();
            }

            return View(formationEmployee);
        }

        // GET: FormationEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            ViewData["FormationId"] = new SelectList(_context.Formation, "FormationId", "Formation_Name");
            return View();
        }

        // POST: FormationEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormationEmployeeId,FormationId,EmployeeId,NotaAtribuida")] FormationEmployee formationEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formationEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", formationEmployee.EmployeeId);
            ViewData["FormationId"] = new SelectList(_context.Formation, "FormationId", "Formation_Name", formationEmployee.FormationId);
            return View(formationEmployee);
        }

        // GET: FormationEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormationEmployee == null)
            {
                return NotFound();
            }

            var formationEmployee = await _context.FormationEmployee.FindAsync(id);
            if (formationEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", formationEmployee.EmployeeId);
            ViewData["FormationId"] = new SelectList(_context.Formation, "FormationId", "Formation_Name", formationEmployee.FormationId);
            return View(formationEmployee);
        }

        // POST: FormationEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormationEmployeeId,FormationId,EmployeeId,NotaAtribuida")] FormationEmployee formationEmployee)
        {
            if (id != formationEmployee.FormationEmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formationEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationEmployeeExists(formationEmployee.FormationEmployeeId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", formationEmployee.EmployeeId);
            ViewData["FormationId"] = new SelectList(_context.Formation, "FormationId", "Formation_Name", formationEmployee.FormationId);
            return View(formationEmployee);
        }

        // GET: FormationEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormationEmployee == null)
            {
                return NotFound();
            }

            var formationEmployee = await _context.FormationEmployee
                .Include(f => f.Employee)
                .Include(f => f.Formation)
                .FirstOrDefaultAsync(m => m.FormationEmployeeId == id);
            if (formationEmployee == null)
            {
                return NotFound();
            }

            return View(formationEmployee);
        }

        // POST: FormationEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormationEmployee == null)
            {
                return Problem("Entity set 'SupermarketDbContext.FormationEmployee'  is null.");
            }
            var formationEmployee = await _context.FormationEmployee.FindAsync(id);
            if (formationEmployee != null)
            {
                _context.FormationEmployee.Remove(formationEmployee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationEmployeeExists(int id)
        {
          return (_context.FormationEmployee?.Any(e => e.FormationEmployeeId == id)).GetValueOrDefault();
        }
    }
}
