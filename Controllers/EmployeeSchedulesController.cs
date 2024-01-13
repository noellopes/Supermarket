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

        // GET: EmployeeSchedules
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.EmployeeSchedule.Include(e => e.Employee);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: EmployeeSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: EmployeeSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeScheduleId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchTime")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Edit/5
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeScheduleId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchTime")] EmployeeSchedule employeeSchedule)
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

        // GET: EmployeeSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

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
