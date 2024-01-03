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
    public class AvailabilitySuppliersController : Controller
    {
        private readonly SupermarketDbContext _context;

        public AvailabilitySuppliersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: AvailabilitySuppliers
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.AvailabilitySupplier.Include(a => a.Product).Include(a => a.Supplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: AvailabilitySuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AvailabilitySupplier == null)
            {
                return NotFound();
            }

            var availabilitySupplier = await _context.AvailabilitySupplier
                .Include(a => a.Product)
                .Include(a => a.Supplier)
                .FirstOrDefaultAsync(m => m.SupplierAvailabilityID == id);
            if (availabilitySupplier == null)
            {
                return NotFound();
            }

            return View(availabilitySupplier);
        }

        // GET: AvailabilitySuppliers/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name");
            return View();
        }

        // POST: AvailabilitySuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierAvailabilityID,ProductId,ProductQuantity,DeliveryTime,SupplierId")] AvailabilitySupplier availabilitySupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availabilitySupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", availabilitySupplier.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name", availabilitySupplier.SupplierId);
            return View(availabilitySupplier);
        }

        // GET: AvailabilitySuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AvailabilitySupplier == null)
            {
                return NotFound();
            }

            var availabilitySupplier = await _context.AvailabilitySupplier.FindAsync(id);
            if (availabilitySupplier == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", availabilitySupplier.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name", availabilitySupplier.SupplierId);
            return View(availabilitySupplier);
        }

        // POST: AvailabilitySuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierAvailabilityID,ProductId,ProductQuantity,DeliveryTime,SupplierId")] AvailabilitySupplier availabilitySupplier)
        {
            if (id != availabilitySupplier.SupplierAvailabilityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availabilitySupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilitySupplierExists(availabilitySupplier.SupplierAvailabilityID))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", availabilitySupplier.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name", availabilitySupplier.SupplierId);
            return View(availabilitySupplier);
        }

        // GET: AvailabilitySuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AvailabilitySupplier == null)
            {
                return NotFound();
            }

            var availabilitySupplier = await _context.AvailabilitySupplier
                .Include(a => a.Product)
                .Include(a => a.Supplier)
                .FirstOrDefaultAsync(m => m.SupplierAvailabilityID == id);
            if (availabilitySupplier == null)
            {
                return NotFound();
            }

            return View(availabilitySupplier);
        }

        // POST: AvailabilitySuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AvailabilitySupplier == null)
            {
                return Problem("Entity set 'SupermarketDbContext.AvailabilitySupplier'  is null.");
            }
            var availabilitySupplier = await _context.AvailabilitySupplier.FindAsync(id);
            if (availabilitySupplier != null)
            {
                _context.AvailabilitySupplier.Remove(availabilitySupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilitySupplierExists(int id)
        {
          return (_context.AvailabilitySupplier?.Any(e => e.SupplierAvailabilityID == id)).GetValueOrDefault();
        }
    }
}
