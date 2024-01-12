using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class EmployeeSchedulesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public EmployeeSchedulesController(SupermarketDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> HRHome(bool? funcionario)
        {
            if (User.IsInRole("Gestor") && (funcionario is null || funcionario == false))
            {
                return View("HRHomeAdmin");
            }
            else
            {
                return View("HRHome");
            }
        }
        
        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int page = 1)
        {
            var funcionario = await _context.Employee.Where(x => x.Employee_Email == User.Identity.Name).FirstOrDefaultAsync();
            if(funcionario is not null)
            {
                var employeeSchedulesQuery = _context.EmployeeSchedule.AsQueryable();

                if (startDate.HasValue && endDate.HasValue)
                {
                    employeeSchedulesQuery = employeeSchedulesQuery.Where(e => e.Date >= startDate && e.Date <= endDate);
                }

                var list = await employeeSchedulesQuery.Where(s=>s.EmployeeId==funcionario.EmployeeId).ToListAsync();
                var vm = new EmployeeSchedulesViewModel
                {
                    EmployeeSchedules = list.Skip((page - 1) * 10).Take(10).ToList(),
                    StartDate = startDate,
                    EndDate = endDate,
                    PageIndex = page,
                    TotalPages = (int)Math.Ceiling(list.Count() / 10.0)
                };

                ViewData["Employee"] = funcionario.Employee_Name;
                return View("Index", vm);
            }
            else
            {
                return View("HRHome");
            }
        }

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> IndexAdmin(int? employeeId, DateTime? startDate, DateTime? endDate, int page = 1)
        {
            var employeeSchedulesQuery = _context.EmployeeSchedule.AsQueryable();

            if (employeeId.HasValue)
            {
                employeeSchedulesQuery = employeeSchedulesQuery.Where(e => e.EmployeeId == employeeId);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                employeeSchedulesQuery = employeeSchedulesQuery.Where(e => e.Date >= startDate && e.Date <= endDate);
            }

            var list = await employeeSchedulesQuery.Include(e => e.Employee).ToListAsync();

            var vm = new EmployeeSchedulesViewModel
            {
                EmployeeSchedules = list.Skip((page - 1) * 10).Take(10).ToList(),
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate,
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(list.Count() / 10.0)
            };

            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", employeeId);

            return View(vm);
        }


        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }
            ViewData["Employee"] = _context.Employee.Where(s=>s.EmployeeId==employeeSchedule.EmployeeId).FirstOrDefault()?.Employee_Name ?? "Funcioário X";
            return View(employeeSchedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("CheckInTime,CheckOutTime,LunchStartTime,LunchTime")] EmployeeSchedule employeeSchedule)
        {
            if (id != employeeSchedule.EmployeeScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeScheduleExists(employeeSchedule.EmployeeScheduleId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeSchedule == null)
            {
                return Problem("Entity set 'SupermarketDbContext.EmployeeSchedule'  is null.");
            }
            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule != null)
            {
                _context.EmployeeSchedule.Remove(employeeSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeScheduleExists(int id)
        {
          return (_context.EmployeeSchedule?.Any(e => e.EmployeeScheduleId == id)).GetValueOrDefault();
        }
    }
}
