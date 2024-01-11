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
    public class PurchaseProductSuppliersController : Controller
    {
        private readonly SupermarketDbContext _context;

        public PurchaseProductSuppliersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseProductSuppliers
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.PurchaseProductSupplier.Include(p => p.Product).Include(p => p.PurchaseSupplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: PurchaseProductSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseProductSupplier == null)
            {
                return NotFound();
            }

            var purchaseProductSupplier = await _context.PurchaseProductSupplier
                .Include(p => p.Product)
                .Include(p => p.PurchaseSupplier)
                .FirstOrDefaultAsync(m => m.PurchaseProductSupplierId == id);
            if (purchaseProductSupplier == null)
            {
                return NotFound();
            }

            return View(purchaseProductSupplier);
        }

        // GET: PurchaseProductSuppliers/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name");
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "Date");
            return View();
        }

        // POST: PurchaseProductSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseProductSupplierId,ProductId,PurchaseSupplierId,AskedQuantity,DeliveredQuantity,DeliveredDate,LineTotal")] PurchaseProductSupplier purchaseProductSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseProductSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", purchaseProductSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "Date", purchaseProductSupplier.PurchaseSupplierId);
            return View(purchaseProductSupplier);
        }

        // GET: PurchaseProductSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseProductSupplier == null)
            {
                return NotFound();
            }

            var purchaseProductSupplier = await _context.PurchaseProductSupplier.FindAsync(id);
            if (purchaseProductSupplier == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", purchaseProductSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "Date", purchaseProductSupplier.PurchaseSupplierId);
            return View(purchaseProductSupplier);
        }

        // POST: PurchaseProductSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseProductSupplierId,ProductId,PurchaseSupplierId,AskedQuantity,DeliveredQuantity,DeliveredDate,LineTotal")] PurchaseProductSupplier purchaseProductSupplier)
        {
            if (id != purchaseProductSupplier.PurchaseProductSupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseProductSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseProductSupplierExists(purchaseProductSupplier.PurchaseProductSupplierId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", purchaseProductSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "Date", purchaseProductSupplier.PurchaseSupplierId);
            return View(purchaseProductSupplier);
        }

        // GET: PurchaseProductSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseProductSupplier == null)
            {
                return NotFound();
            }

            var purchaseProductSupplier = await _context.PurchaseProductSupplier
                .Include(p => p.Product)
                .Include(p => p.PurchaseSupplier)
                .FirstOrDefaultAsync(m => m.PurchaseProductSupplierId == id);
            if (purchaseProductSupplier == null)
            {
                return NotFound();
            }

            return View(purchaseProductSupplier);
        }

        // POST: PurchaseProductSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseProductSupplier == null)
            {
                return Problem("Entity set 'SupermarketDbContext.PurchaseProductSupplier'  is null.");
            }
            var purchaseProductSupplier = await _context.PurchaseProductSupplier.FindAsync(id);
            if (purchaseProductSupplier != null)
            {
                _context.PurchaseProductSupplier.Remove(purchaseProductSupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseProductSupplierExists(int id)
        {
          return (_context.PurchaseProductSupplier?.Any(e => e.PurchaseProductSupplierId == id)).GetValueOrDefault();
        }
    }
}
