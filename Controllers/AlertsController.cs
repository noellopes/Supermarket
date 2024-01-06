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
    public class AlertsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public AlertsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Alerts
        public async Task<IActionResult> Index()
        {
              return _context.Alert != null ? 
                          View(await _context.Alert.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Alert'  is null.");
        }

        // GET: Alerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alert == null)
            {
                return NotFound();
            }

            var alert = await _context.Alert
                .FirstOrDefaultAsync(m => m.AlertId == id);

            alert!.Status = "Seen";
            _context.Update(alert);
            await _context.SaveChangesAsync();

            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // GET: Alerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AlertId,Role,Status,Date,Description")] Alert alert)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(alert);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(alert);
        //}

        //// GET: Alerts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Alert == null)
        //    {
        //        return NotFound();
        //    }

        //    var alert = await _context.Alert.FindAsync(id);
        //    if (alert == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(alert);
        //}

        //// POST: Alerts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("AlertId,Role,Status,Date,Description")] Alert alert)
        //{
        //    if (id != alert.AlertId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(alert);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AlertExists(alert.AlertId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(alert);
        //}

        // GET: Alerts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Alert == null)
        //    {
        //        return NotFound();
        //    }

        //    var alert = await _context.Alert
        //        .FirstOrDefaultAsync(m => m.AlertId == id);
        //    if (alert == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(alert);
        //}

        //// POST: Alerts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Alert == null)
        //    {
        //        return Problem("Entity set 'SupermarketDbContext.Alert'  is null.");
        //    }
        //    var alert = await _context.Alert.FindAsync(id);
        //    if (alert != null)
        //    {
        //        _context.Alert.Remove(alert);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AlertExists(int id)
        {
          return (_context.Alert?.Any(e => e.AlertId == id)).GetValueOrDefault();
        }
    }
}
