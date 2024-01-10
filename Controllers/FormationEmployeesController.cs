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
        public async Task<IActionResult> Index(int page = 1, string employeeName = "", string formationName = "")
        {
            var formationEmployees = from fe in _context.FormationEmployee
                                     .Include(fe => fe.Employee)
                                     .Include(fe => fe.Formation)
                                     select fe;

            if (employeeName != "")
            {
                formationEmployees = formationEmployees.Where(fe => fe.Employee.Employee_Name.Contains(employeeName));
            }

            if (formationName != "")
            {
                formationEmployees = formationEmployees.Where(fe => fe.Formation.Formation_Name.Contains(formationName));
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await formationEmployees.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new FormationEmployeeViewModel
            {
                FormationEmployees = await formationEmployees
                    .OrderBy(fe => fe.Employee.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                SearchEmployeeName = employeeName,
                SearchFormationName = formationName,
            };

            return View(vm);
        }
        // GET: FormationEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", formationEmployee.EmployeeId);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", formationEmployee.EmployeeId);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", formationEmployee.EmployeeId);
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