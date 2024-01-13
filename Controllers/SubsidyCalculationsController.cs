using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class SubsidyCalculationsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public SubsidyCalculationsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: SubsidyCalculations
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.SubsidyCalculation.Include(s => s.Ponto);

            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: SubsidyCalculations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation
                .Include(s => s.Ponto)
                //.Include(s => s.SubsidySetup)
                .FirstOrDefaultAsync(m => m.SubsidyCalculationId == id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }

            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Create
        public IActionResult Create()
        {
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status");
            ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId");
            return View();
        }

        // POST: SubsidyCalculations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubsidyCalculationId,TotalHoras,PontoId,SubsidySetupId")] SubsidyCalculation subsidyCalculation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidyCalculation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //  ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation.FindAsync(id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //  ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // POST: SubsidyCalculations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubsidyCalculationId,TotalHoras,PontoId,SubsidySetupId")] SubsidyCalculation subsidyCalculation)
        {
            if (id != subsidyCalculation.SubsidyCalculationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidyCalculation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidyCalculationExists(subsidyCalculation.SubsidyCalculationId))
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
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation
                .Include(s => s.Ponto)
                // .Include(s => s.SubsidySetup)
                .FirstOrDefaultAsync(m => m.SubsidyCalculationId == id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }

            return View(subsidyCalculation);
        }

        // POST: SubsidyCalculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubsidyCalculation == null)
            {
                return Problem("Entity set 'SupermarketDbContext.SubsidyCalculation'  is null.");
            }
            var subsidyCalculation = await _context.SubsidyCalculation.FindAsync(id);
            if (subsidyCalculation != null)
            {
                _context.SubsidyCalculation.Remove(subsidyCalculation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsidyCalculationExists(int id)
        {
            return (_context.SubsidyCalculation?.Any(e => e.SubsidyCalculationId == id)).GetValueOrDefault();
        }
    }
}
