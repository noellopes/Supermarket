﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Migrations.Supermarket;
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
            var Evaluations = _context.AvaliacaoFuncionarios.Include(ee => ee.Employee);
            return Evaluations != null ? 
                          View(await Evaluations.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.AvaliacaoFuncionarios'  is null.");
        }

        // GET: EmployeeEvaluations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AvaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.AvaliacaoFuncionarios
                .Include(ee => ee.Employee)
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
            ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: EmployeeEvaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeEvaluationId,Description,GradeNumber,EmployeeId")] Supermarket.Models.EmployeeEvaluation employeeEvaluation)
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
            if (id == null || _context.AvaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.AvaliacaoFuncionarios.FindAsync(id);
            if (employeeEvaluation == null)
            {
                return NotFound();
            }
            ViewData["EmployeesList"] = new SelectList(_context.Set<Employee>(), "EmployeeId", "Employee_Name");
            return View(employeeEvaluation);
        }

        // POST: EmployeeEvaluations/Edit/5
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
            if (id == null || _context.AvaliacaoFuncionarios == null)
            {
                return NotFound();
            }

            var employeeEvaluation = await _context.AvaliacaoFuncionarios
                .Include(ee => ee.Employee)
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
            if (_context.AvaliacaoFuncionarios == null)
            {
                return Problem("Entity set 'SupermarketDbContext.avaliacaoFuncionarios'  is null.");
            }
            var employeeEvaluation = await _context.AvaliacaoFuncionarios.FindAsync(id);
            if (employeeEvaluation != null)
            {
                _context.AvaliacaoFuncionarios.Remove(employeeEvaluation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeEvaluationExists(int id)
        {
          return (_context.AvaliacaoFuncionarios?.Any(e => e.EmployeeEvaluationId == id)).GetValueOrDefault();
        }
    }
}