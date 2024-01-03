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
    public class ProductPurchaseSuppliersController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ProductPurchaseSuppliersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ProductPurchaseSuppliers
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ProductPurchaseSupplier.Include(p => p.Product).Include(p => p.PurchaseSupplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ProductPurchaseSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductPurchaseSupplier == null)
            {
                return NotFound();
            }

            var productPurchaseSupplier = await _context.ProductPurchaseSupplier
                .Include(p => p.Product)
                .Include(p => p.PurchaseSupplier)
                .FirstOrDefaultAsync(m => m.ProductPurchaseSupplierId == id);
            if (productPurchaseSupplier == null)
            {
                return NotFound();
            }

            return View(productPurchaseSupplier);
        }

        // GET: ProductPurchaseSuppliers/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description");
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "PurchaseSupplierId");
            return View();
        }

        // POST: ProductPurchaseSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPurchaseSupplierId,ProductId,PurchaseSupplierId,AskedQuantity,DeliveredQuantity,EstimateDeliveryTime,DeliveredDate,LineTotal")] ProductPurchaseSupplier productPurchaseSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPurchaseSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productPurchaseSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "PurchaseSupplierId", productPurchaseSupplier.PurchaseSupplierId);
            return View(productPurchaseSupplier);
        }

        // GET: ProductPurchaseSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductPurchaseSupplier == null)
            {
                return NotFound();
            }

            var productPurchaseSupplier = await _context.ProductPurchaseSupplier.FindAsync(id);
            if (productPurchaseSupplier == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productPurchaseSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "PurchaseSupplierId", productPurchaseSupplier.PurchaseSupplierId);
            return View(productPurchaseSupplier);
        }

        // POST: ProductPurchaseSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPurchaseSupplierId,ProductId,PurchaseSupplierId,AskedQuantity,DeliveredQuantity,EstimateDeliveryTime,DeliveredDate,LineTotal")] ProductPurchaseSupplier productPurchaseSupplier)
        {
            if (id != productPurchaseSupplier.ProductPurchaseSupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPurchaseSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPurchaseSupplierExists(productPurchaseSupplier.ProductPurchaseSupplierId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Description", productPurchaseSupplier.ProductId);
            ViewData["PurchaseSupplierId"] = new SelectList(_context.PurchaseSupplier, "PurchaseSupplierId", "PurchaseSupplierId", productPurchaseSupplier.PurchaseSupplierId);
            return View(productPurchaseSupplier);
        }

        // GET: ProductPurchaseSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductPurchaseSupplier == null)
            {
                return NotFound();
            }

            var productPurchaseSupplier = await _context.ProductPurchaseSupplier
                .Include(p => p.Product)
                .Include(p => p.PurchaseSupplier)
                .FirstOrDefaultAsync(m => m.ProductPurchaseSupplierId == id);
            if (productPurchaseSupplier == null)
            {
                return NotFound();
            }

            return View(productPurchaseSupplier);
        }

        // POST: ProductPurchaseSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductPurchaseSupplier == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ProductPurchaseSupplier'  is null.");
            }
            var productPurchaseSupplier = await _context.ProductPurchaseSupplier.FindAsync(id);
            if (productPurchaseSupplier != null)
            {
                _context.ProductPurchaseSupplier.Remove(productPurchaseSupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPurchaseSupplierExists(int id)
        {
          return (_context.ProductPurchaseSupplier?.Any(e => e.ProductPurchaseSupplierId == id)).GetValueOrDefault();
        }
    }
}
