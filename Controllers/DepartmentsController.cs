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
        private readonly GroupsDbContext _context;

        public DepartmentsController(GroupsDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.Departments != null ? 
                          View(await _context.Departments.ToListAsync()) :
                          Problem("Entity set 'GroupsDbContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .FirstOrDefaultAsync(m => m.IDDepartments == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                bool DepartmentsExists = await _context.Departments.AnyAsync(
                d => d.NameDepartments == departments.NameDepartments);

                //&& b.Adress == name.Adress && b.nameId != name.NameId);

                if (DepartmentsExists)
                {
                    ModelState.AddModelError("", "Another Departments with the same Name already exists.");
                }

                else { 
                    _context.Add(departments);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Departamento successfully Create.";
                    //book.Author = await _context.Author.FindAsync(book.AuthorId);

                    return View("Details", departments);
                }
            }
            //ViewData["IDDepartments"] = new SelectList(_context.Set<Schedule>(), "ScheduleId ", "StateSchedule", Schedule.ScheduleId);
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
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Departments departments)
        {
            if (id != departments.IDDepartments)
            {
                return View("Departmentsnotfound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool boolExists = await _context.Departments.AnyAsync(
                    d => d.NameDepartments == departments.NameDepartments);

                    if (boolExists)
                    {
                        ModelState.AddModelError("", "Another Department with same Name Department already exist");
                    }
                    else
                    {
                        _context.Update(departments);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Department sucessfully edit.";
                        departments.NameDepartments = await _context.NameDepartments.FindAsync(departments.IDDepartments);
                        return View("Details", departments);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.IDDepartments))
                    {
                        return View("Departmentnotfound");
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["IDDepartments"] = new SelectList(_context.Set<Departments>(), "IDDepartments", "NameDepartments", departments.IDDepartments);
                return RedirectToAction(nameof(Index));
            }
            return View(departments);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
               return View("DeleteConfirmed");
            }

            var departments = await _context.Departments
                .Include(d => d.NameDepartments)
                .FirstOrDefaultAsync(m => m.IDDepartments == id);
            if (departments == null)
            {
                return View("DeleteConfirmed", departments);
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
                return Problem("Entity set 'GroupsDbContext.Departments'  is null.");
            }
            var departments = await _context.Departments.FindAsync(id);
            if (departments != null)
            {
                _context.Departments.Remove(departments);
            }
            
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            departments.NameDepartments = await _context.NameDepartments.FindAsync(departments.IDDepartments);
            return View("DeleteConfirmed", departments);
        }

        private bool DepartmentsExists(int id)
        {
          return (_context.Departments?.Any(e => e.IDDepartments == id)).GetValueOrDefault();
        }
    }
}
