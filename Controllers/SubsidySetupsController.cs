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
    public class SubsidySetupsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public SubsidySetupsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: SubsidySetups
        public async Task<IActionResult> Index()
        {
              return _context.SubsidySetup != null ? 
                          View(await _context.SubsidySetup.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.SubsidySetup'  is null.");
        }

        // GET: SubsidySetups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubsidySetup == null)
            {
                return NotFound();
            }

            var subsidySetup = await _context.SubsidySetup
                .FirstOrDefaultAsync(m => m.SubsidySetupId == id);
            if (subsidySetup == null)
            {
                return NotFound();
            }

            return View(subsidySetup);
        }

        // GET: SubsidySetups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubsidySetups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubsidySetupId,HorasMinTrabalhadas,valorSubsidioDiario,DataPagamentoMensal")] SubsidySetup subsidySetup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidySetup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subsidySetup);
        }

        // GET: SubsidySetups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubsidySetup == null)
            {
                return NotFound();
            }

            var subsidySetup = await _context.SubsidySetup.FindAsync(id);
            if (subsidySetup == null)
            {
                return NotFound();
            }
            return View(subsidySetup);
        }

        // POST: SubsidySetups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubsidySetupId,HorasMinTrabalhadas,valorSubsidioDiario,DataPagamentoMensal")] SubsidySetup subsidySetup)
        {
            if (id != subsidySetup.SubsidySetupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidySetup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidySetupExists(subsidySetup.SubsidySetupId))
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
            return View(subsidySetup);
        }

        // GET: SubsidySetups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubsidySetup == null)
            {
                return NotFound();
            }

            var subsidySetup = await _context.SubsidySetup
                .FirstOrDefaultAsync(m => m.SubsidySetupId == id);
            if (subsidySetup == null)
            {
                return NotFound();
            }

            return View(subsidySetup);
        }

        // POST: SubsidySetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubsidySetup == null)
            {
                return Problem("Entity set 'SupermarketDbContext.SubsidySetup'  is null.");
            }
            var subsidySetup = await _context.SubsidySetup.FindAsync(id);
            if (subsidySetup != null)
            {
                _context.SubsidySetup.Remove(subsidySetup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsidySetupExists(int id)
        {
          return (_context.SubsidySetup?.Any(e => e.SubsidySetupId == id)).GetValueOrDefault();
        }
    }
}
