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

        // GET: EmployeeEvaluations
        public async Task<IActionResult> Index()
        {
              return _context.avaliacaoFuncionarios != null ? 
                          View(await _context.avaliacaoFuncionarios.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.avaliacaoFuncionarios'  is null.");
        }

        // GET: EmployeeEvaluations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.avaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.avaliacaoFuncionarios
                .FirstOrDefaultAsync(m => m.EmployeeEvaluationId == id);
            if (employeeEvaluation == null)
            {
                return NotFound();
            }

            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeEvaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeEvaluationId,Description,GradeNumber,EmployeeId")] EmployeeEvaluation employeeEvaluation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeEvaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.avaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.avaliacaoFuncionarios.FindAsync(id);
            if (employeeEvaluation == null)
            {
                return NotFound();
            }
            return View(employeeEvaluation);
        }

        // POST: EmployeeEvaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeEvaluationId,Description,GradeNumber,EmployeeId")] EmployeeEvaluation employeeEvaluation)
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
                return RedirectToAction(nameof(Index));
            }
            return View(employeeEvaluation);
        }

        // GET: EmployeeEvaluations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.avaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.avaliacaoFuncionarios
                .FirstOrDefaultAsync(m => m.EmployeeEvaluationId == id);
            if (employeeEvaluation == null)
            {
                return NotFound();
            }

            return View(employeeEvaluation);
        }

        // POST: EmployeeEvaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.avaliacaoFuncionarios == null)
            {
                return Problem("Entity set 'SupermarketDbContext.avaliacaoFuncionarios'  is null.");
            }
            var employeeEvaluation = await _context.avaliacaoFuncionarios.FindAsync(id);
            if (employeeEvaluation != null)
            {
                _context.avaliacaoFuncionarios.Remove(employeeEvaluation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeEvaluationExists(int id)
        {
          return (_context.avaliacaoFuncionarios?.Any(e => e.EmployeeEvaluationId == id)).GetValueOrDefault();
        }
    }
}
