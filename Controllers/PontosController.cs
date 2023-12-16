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
    public class PontosController : Controller
    {
        private readonly SupermarketDbContext _context;

        public PontosController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Pontos
        public async Task<IActionResult> Index()
        {
              
            return _context.Ponto != null ? 
                          View(await _context.Ponto.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Ponto'  is null.");
        }

        // GET: Pontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto
                .FirstOrDefaultAsync(m => m.PontoId == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // GET: Pontos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pontos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PontoId,EmployeeId,CheckInTime,CheckOutTime,LunchStartTime,LunchEndTime,DayBalance,Status,Justificative,CheckInCoordenates,CheckOutCoordenates")] Ponto ponto)
        {
            if (ModelState.IsValid)
            {
                ponto.Date = DateTime.Now;
                ponto.CheckInTime = DateTime.Now.TimeOfDay;
                ponto.CheckOutTime = DateTime.Now.TimeOfDay;
                ponto.LunchStartTime = DateTime.Now.TimeOfDay;
                ponto.LunchEndTime = DateTime.Now.TimeOfDay;
                _context.Add(ponto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ponto);
        }

        // GET: Pontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto.FindAsync(id);
            if (ponto == null)
            {
                return NotFound();
            }
            return View(ponto);
        }

        // POST: Pontos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PontoId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchEndTime,DayBalance,Status,Justificative,CheckInCoordenates,CheckOutCoordenates")] Ponto ponto)
        {
            if (id != ponto.PontoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ponto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoExists(ponto.PontoId))
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
            return View(ponto);
        }

        // GET: Pontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto
                .FirstOrDefaultAsync(m => m.PontoId == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // POST: Pontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ponto == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Ponto'  is null.");
            }
            var ponto = await _context.Ponto.FindAsync(id);
            if (ponto != null)
            {
                _context.Ponto.Remove(ponto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoExists(int id)
        {
          return (_context.Ponto?.Any(e => e.PontoId == id)).GetValueOrDefault();
        }
    }
}
