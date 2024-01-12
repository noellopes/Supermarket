using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public QualificationsController(SupermarketDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, string employee = "", string department = "", int level = 0)
        {
            var qualification = _context.Qualification
                .Include(q => q.Employee)
                .Include(q => q.Departments)
                .AsQueryable();

            Console.WriteLine(employee);
            if (employee != "")
            {
                Console.WriteLine(employee);
                qualification = qualification.Where(q => q.Employee != null && q.Employee.Employee_Name.Contains(employee));
            } 

            if (department != "")
            {
                Console.WriteLine(department);
                qualification = qualification.Where(q => q.Departments != null && q.Departments.NameDepartments.Contains(department));
            }

            if (level != 0)
            {
                qualification = qualification.Where(q => q.Level == level);
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await qualification.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new QualificationListViewModel
            {
                Qualifications = await qualification
                    .OrderBy(r => r.QualificationId)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                Pagination = paging,
                SearchNameEmp = employee,
                SearchNameDep = department

            };

            return View(vm);
        }

        // GET: Qualifications
        

        // GET: Qualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Qualification == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification
                .Include(q => q.Departments)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(m => m.QualificationId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // GET: Qualifications/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "IDDepartments", "NameDepartments");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: Qualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QualificationId,EmployeeId,DepartmentsN,DepartmentId,Level")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualification);
                await _context.SaveChangesAsync();
                return RedirectToAction();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "IDDepartments", "NameDepartments", qualification.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", qualification.EmployeeId);
            return View(qualification);
        }

        // GET: Qualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Qualification == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "IDDepartments", "NameDepartments", qualification.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", qualification.EmployeeId);
            return View(qualification);
        }

        // POST: Qualifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualificationId,EmployeeId,DepartmentsN,DepartmentId,Level")] Qualification qualification)
        {
            if (id != qualification.QualificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationExists(qualification.QualificationId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "IDDepartments", "NameDepartments", qualification.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", qualification.EmployeeId);
            return View(qualification);
        }

        // GET: Qualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Qualification == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification
                .Include(q => q.Departments)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(m => m.QualificationId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // POST: Qualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Qualification == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Qualification'  is null.");
            }
            var qualification = await _context.Qualification.FindAsync(id);
            if (qualification != null)
            {
                _context.Qualification.Remove(qualification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualificationExists(int id)
        {
          return (_context.Qualification?.Any(e => e.QualificationId == id)).GetValueOrDefault();
        }
    }
}
