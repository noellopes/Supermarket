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
    public class SupplierAvailabilityController : Controller
    {
        private readonly SupermarketDbContext _context;

        public SupplierAvailabilityController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: SupplierAvailability
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.SupplierAvailability.Include(s => s.Supplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: SupplierAvailability/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SupplierAvailability == null)
            {
                return NotFound();
            }

            var supplierAvailability = await _context.SupplierAvailability
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SupplierAvailabilityID == id);
            if (supplierAvailability == null)
            {
                return NotFound();
            }

            return View(supplierAvailability);
        }

        // GET: SupplierAvailability/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "HeadQuarters");
            return View();
        }

        // POST: SupplierAvailability/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierAvailabilityID,Name,UnitPriceSupplier,ProductQuantity,DeliveryTime,SupplierId")] SupplierAvailability supplierAvailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "HeadQuarters", supplierAvailability.SupplierId);
            return View(supplierAvailability);
        }

        // GET: SupplierAvailability/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SupplierAvailability == null)
            {
                return NotFound();
            }

            var supplierAvailability = await _context.SupplierAvailability.FindAsync(id);
            if (supplierAvailability == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "HeadQuarters", supplierAvailability.SupplierId);
            return View(supplierAvailability);
        }

        // POST: SupplierAvailability/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierAvailabilityID,Name,UnitPriceSupplier,ProductQuantity,DeliveryTime,SupplierId")] SupplierAvailability supplierAvailability)
        {
            if (id != supplierAvailability.SupplierAvailabilityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierAvailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierAvailabilityExists(supplierAvailability.SupplierAvailabilityID))
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
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "HeadQuarters", supplierAvailability.SupplierId);
            return View(supplierAvailability);
        }

        // GET: SupplierAvailability/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SupplierAvailability == null)
            {
                return NotFound();
            }

            var supplierAvailability = await _context.SupplierAvailability
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SupplierAvailabilityID == id);
            if (supplierAvailability == null)
            {
                return NotFound();
            }

            return View(supplierAvailability);
        }

        // POST: SupplierAvailability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SupplierAvailability == null)
            {
                return Problem("Entity set 'SupermarketDbContext.SupplierAvailability'  is null.");
            }
            var supplierAvailability = await _context.SupplierAvailability.FindAsync(id);
            if (supplierAvailability != null)
            {
                _context.SupplierAvailability.Remove(supplierAvailability);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierAvailabilityExists(int id)
        {
          return (_context.SupplierAvailability?.Any(e => e.SupplierAvailabilityID == id)).GetValueOrDefault();
        }
    }
}
