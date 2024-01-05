using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    [Authorize(Roles = "Avaliar_Funcionarios")]
    public class EmployeeEvaluationsController : Controller
    {
        private readonly SupermarketDbContext _context;
        private readonly UserManager<IdentityUser>? _userManager;

        public EmployeeEvaluationsController(SupermarketDbContext context, UserManager<IdentityUser>? userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EmployeeEvaluation
        public async Task<IActionResult> IndexAsync(int page = 1, string description = "", string employeeName = "", int employeeId = 0)
        {
            var evaluationsFiltered = _context.EmployeeEvaluation.Include(ee => ee.Employee).AsQueryable();

            IdentityUser? user = _userManager!.GetUserAsync(User).Result;
            //Remove own employee evaluations
            if (await _userManager!.IsInRoleAsync(user!, "Role_Funcionario"))
            {
                evaluationsFiltered = evaluationsFiltered.Where(ee => ee.Employee!.Employee_Email != user!.UserName);
            }

            //Description filter
            if (description != "")
            {
                evaluationsFiltered = evaluationsFiltered.Where(ee => ee.Description!.Contains(description));
            }

            //Employee name filter
            if (employeeName != "")
            {
                evaluationsFiltered = evaluationsFiltered.Where(ee => ee.Employee!.Employee_Name.Contains(employeeName));
            }

            //Employee filter
            if (employeeId > 0)
            {
                //cannot show own evaluations
                if (user!.UserName == _context.Employee.Find(employeeId)!.Employee_Email)
                {
                    return View("Unauthorized");
                }
                evaluationsFiltered = evaluationsFiltered.Where(ee => ee.Employee!.EmployeeId == employeeId);
                ViewBag.EmployeeName = _context.Employee.Find(employeeId)!.Employee_Name;
                ViewBag.EmployeeId = employeeId;
                ViewBag.AvgGrade = EmployeeGradeAsync(employeeId);
            }

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = evaluationsFiltered.Count()
            };

            ViewBag.FilterDescription = description;
            ViewBag.FilterEmployeeName = employeeName;

            return View(
                new EmployeeEvaluationListViewModel
                {
                    EmployeeEvaluation = evaluationsFiltered.OrderByDescending(ee => ee.EvaluationDate)
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
                return RedirectToAction(nameof(IndexAsync));
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluation/Create
        public IActionResult Create(int employeeId = 0)
        {
            if(employeeId > 0)
            {
                ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>().Where(ee=>ee.EmployeeId==employeeId), "EmployeeId", "Employee_Name");
                
                ViewBag.EmployeeId = employeeId;
            }
            else
            {
                ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "Employee_Name");
            }
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
                return RedirectToAction(nameof(IndexAsync));
            }
            ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>().Where(ee => ee.EmployeeId == employeeEvaluation.EmployeeId), "EmployeeId", "Employee_Name");
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
        public async Task<IActionResult> EmployeeListView()
        {
            return _context.Employee != null ?
                        View(await _context.Employee.ToListAsync()) :
                        Problem("Entity set 'SupermarketDbContext.Employee'  is null.");
        }

        private float EmployeeGradeAsync(int? EmployeeId)
        {
            var assiduidade = 1.0;
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
            if (Evaluations.Count < 1)
            {
                //There are no evaluations for this employee
                return 0;
            }
            else
            {
                //TO-DO calcular assiduidade
                var sum = 0;
                foreach (var evaluation in Evaluations)
                {
                    sum += evaluation.GradeNumber;
                }

                var mean = sum / Evaluations.Count;

                mean = (int)(mean * assiduidade);
                return mean;
            }
            
        }
    }
}
