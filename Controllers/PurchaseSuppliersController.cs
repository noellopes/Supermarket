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
    public class PurchaseSuppliersController : Controller
    {
        private readonly SupermarketDbContext _context;

        public PurchaseSuppliersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseSuppliers
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.PurchaseSupplier.Include(p => p.Supplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: PurchaseSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseSupplier == null)
            {
                return NotFound();
            }

            var purchaseSupplier = await _context.PurchaseSupplier
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseSupplierId == id);
            if (purchaseSupplier == null)
            {
                return NotFound();
            }

            return View(purchaseSupplier);
        }

        // GET: PurchaseSuppliers/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name");
            return View();
        }

        // POST: PurchaseSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseSupplierId,SubTotal,Total,Date,SupplierId")] PurchaseSupplier purchaseSupplier)
        {
            
                _context.Add(purchaseSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: PurchaseSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseSupplier == null)
            {
                return NotFound();
            }

            var purchaseSupplier = await _context.PurchaseSupplier.FindAsync(id);
            if (purchaseSupplier == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name", purchaseSupplier.SupplierId);
            return View(purchaseSupplier);
        }

        // POST: PurchaseSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseSupplierId,SubTotal,Total,Date,SupplierId")] PurchaseSupplier purchaseSupplier)
        {
            if (id != purchaseSupplier.PurchaseSupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseSupplierExists(purchaseSupplier.PurchaseSupplierId))
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
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "SupplierId", "Name", purchaseSupplier.SupplierId);
            return View(purchaseSupplier);
        }

        // GET: PurchaseSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseSupplier == null)
            {
                return NotFound();
            }

            var purchaseSupplier = await _context.PurchaseSupplier
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.PurchaseSupplierId == id);
            if (purchaseSupplier == null)
            {
                return NotFound();
            }

            return View(purchaseSupplier);
        }

        // POST: PurchaseSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseSupplier == null)
            {
                return Problem("Entity set 'SupermarketDbContext.PurchaseSupplier'  is null.");
            }
            var purchaseSupplier = await _context.PurchaseSupplier.FindAsync(id);
            if (purchaseSupplier != null)
            {
                _context.PurchaseSupplier.Remove(purchaseSupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseSupplierExists(int id)
        {
          return (_context.PurchaseSupplier?.Any(e => e.PurchaseSupplierId == id)).GetValueOrDefault();
        }
    }
}
