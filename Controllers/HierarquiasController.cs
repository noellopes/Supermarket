using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class HierarquiasController : Controller
    {
        private readonly SupermarketDbContext _context;

        public HierarquiasController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Hierarquias
        public async Task<IActionResult> Index()
        {
            var hierarquias = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .ToListAsync();

            var employeesViewModel = ObterEmployeesViewModel();

            ViewData["EmployeesViewModel"] = employeesViewModel;

            return View(hierarquias);
        }

        private EmployeesViewModel ObterEmployeesViewModel()
        {
            var employees = _context.Employee.ToList();
            return new EmployeesViewModel { Employees = employees };
        }

        // GET: Hierarquias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hierarquia == null)
            {
                return NotFound();
            }

            return View(hierarquia);
        }

        // GET: Hierarquias/Create
        public IActionResult Create()
        {
            PopulateEmployeeDropdowns();
            return View();
        }

        // POST: Hierarquias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SuperiorId,SubordinadoId")] Hierarquias hierarquia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hierarquia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateEmployeeDropdowns();
            return View(hierarquia);
        }

        // GET: Hierarquias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias.FindAsync(id);

            if (hierarquia == null)
            {
                return NotFound();
            }

            PopulateEmployeeDropdowns();
            return View(hierarquia);
        }

        // POST: Hierarquias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuperiorId,SubordinadoId")] Hierarquias hierarquia)
        {
            if (id != hierarquia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hierarquia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HierarquiaExists(hierarquia.Id))
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

            PopulateEmployeeDropdowns();
            return View(hierarquia);
        }

        // GET: Hierarquias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hierarquia == null)
            {
                return NotFound();
            }

            return View(hierarquia);
        }

        // POST: Hierarquias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hierarquia = await _context.Hierarquias.FindAsync(id);
            _context.Hierarquias.Remove(hierarquia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HierarquiaExists(int id)
        {
            return _context.Hierarquias.Any(e => e.Id == id);
        }

        private void PopulateEmployeeDropdowns()
        {
            ViewData["SuperiorId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            ViewData["SubordinadoId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
        }
    }
}
