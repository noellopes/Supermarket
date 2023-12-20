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
    public class FolgasController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FolgasController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Folgas
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Folga.Include(f => f.Funcionario);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Folgas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(m => m.FolgaId == id);
            if (folga == null)
            {
                return NotFound();
            }

            return View(folga);
        }

        // GET: Folgas/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Address");
            return View();
        }

        // POST: Folgas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FolgaId,FuncionarioId,GestorId,Status,DataPedido,DataResultado,DataInicio,DataFim,motivo")] Folga folga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Address", folga.FuncionarioId);
            return View(folga);
        }

        // GET: Folgas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga.FindAsync(id);
            if (folga == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Address", folga.FuncionarioId);
            return View(folga);
        }

        // POST: Folgas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FolgaId,FuncionarioId,GestorId,Status,DataPedido,DataResultado,DataInicio,DataFim,motivo")] Folga folga)
        {
            if (id != folga.FolgaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolgaExists(folga.FolgaId))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Address", folga.FuncionarioId);
            return View(folga);
        }

        // GET: Folgas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(m => m.FolgaId == id);
            if (folga == null)
            {
                return NotFound();
            }

            return View(folga);
        }

        // POST: Folgas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Folga == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Folga'  is null.");
            }
            var folga = await _context.Folga.FindAsync(id);
            if (folga != null)
            {
                _context.Folga.Remove(folga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FolgaExists(int id)
        {
            return (_context.Folga?.Any(e => e.FolgaId == id)).GetValueOrDefault();
        }
    }
}
