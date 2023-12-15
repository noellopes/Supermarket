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
    public class EmployeeEvaluationsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public EmployeeEvaluationsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeEvaluation
        public async Task<IActionResult> Index(int page = 1)
        {
            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = _context.EmployeeEvaluation.Count()
            };

            return View(
                new EmployeeEvaluationListViewModel
                {
                    EmployeeEvaluation = _context.EmployeeEvaluation.Include(ee => ee.Employee).OrderByDescending(ee => ee.EvaluationDate)
                        .Skip((page - 1) * pagination.PageSize).Take(pagination.PageSize),
                    Pagination = pagination
                }
            );
        }

        // GET: EmployeeEvaluation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeEvaluation == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.EmployeeEvaluation
                .Include(ee => ee.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeEvaluationId == id);
            if (employeeEvaluation == null)
            {
                TempData["MessageError"] = "The employee evaluation has already been deleted!";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluation/Create
        public IActionResult Create()
        {
            ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: EmployeeEvaluation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeEvaluationId,Description,GradeNumber,EmployeeId")] Supermarket.Models.EmployeeEvaluation employeeEvaluation)
        {
            if (ModelState.IsValid)
            {   
                employeeEvaluation.EvaluationDate = DateTime.Now;
                _context.Add(employeeEvaluation);
                await _context.SaveChangesAsync();

                ViewBag.Message = "The evaluation has successfully been created!";
                employeeEvaluation.Employee = await _context.Employee.FindAsync(employeeEvaluation.EmployeeId);
                return View("Details", employeeEvaluation);
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeEvaluation == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.EmployeeEvaluation.FindAsync(id);
            if (employeeEvaluation == null)
            {
                TempData["MessageError"] = "The employee evaluation has already been deleted!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "Employee_Name");
            return View(employeeEvaluation);
        }

        // POST: EmployeeEvaluation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeEvaluationId,Description,GradeNumber,EmployeeId")] Models.EmployeeEvaluation employeeEvaluation)
        {
            if (id != employeeEvaluation.EmployeeEvaluationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeEvaluation);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "The evaluation has successfully been edited!";
                    return View("Details", employeeEvaluation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeEvaluationExists(employeeEvaluation.EmployeeEvaluationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluation/Delete/5
        /*
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeEvaluation == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.EmployeeEvaluation
                .Include(ee => ee.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeEvaluationId == id);
            if (employeeEvaluation == null)
            {
                TempData["MessageError"] = "The employee evaluation has already been deleted!";
                return RedirectToAction(nameof(Index));
            }

            return View(employeeEvaluation);
        }
        */

        // POST: EmployeeEvaluation/Delete/5
        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeEvaluation == null)
            {
                return Problem("Entity set 'SupermarketDbContext.EmployeeEvaluation'  is null.");
            }
            var employeeEvaluation = await _context.EmployeeEvaluation.FindAsync(id);
            if (employeeEvaluation != null)
            {
                _context.EmployeeEvaluation.Remove(employeeEvaluation);
            }
            
            await _context.SaveChangesAsync();

            TempData["Message"] = "The employee evaluation has successfully been deleted!";
            return RedirectToAction(nameof(Index));
        }
        */

        private bool EmployeeEvaluationExists(int id)
        {
          return (_context.EmployeeEvaluation?.Any(e => e.EmployeeEvaluationId == id)).GetValueOrDefault();
        }

        // GET: EmployeeEvaluation/EmployeeView
        public async Task<IActionResult> EmployeeView()
        {
            var Employees = _context.Employee.Include(f=>EmployeeGradeAsync(f.EmployeeId));

            return Employees != null ?
                          View(await Employees.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.EmployeeEvaluation'  is null.");
        }

        private float EmployeeGradeAsync(int? EmployeeId)
        {
            if (EmployeeId == null || _context.Employee == null)
            {
                //The employee doesn't exist!
                return 0;
            }

            var Employee =  _context.Employee.Find(EmployeeId);
            if (Employee == null)
            {
                //The employee doesn't exist!
                return 0;
            }

            var Evaluations = _context.EmployeeEvaluation.Where(af => af.EmployeeId==Employee.EmployeeId).ToList();
            var sum = 0;
            foreach (var evaluation in Evaluations) 
            {
                sum += evaluation.GradeNumber;
            }

            var mean = sum/ Evaluations.Count;
            return mean;
        }
    }
}
