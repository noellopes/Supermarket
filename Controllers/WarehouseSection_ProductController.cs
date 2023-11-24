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
    public class WarehouseSection_ProductController : Controller
    {
        private readonly SupermarketDbContext _context;

        public WarehouseSection_ProductController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: WarehouseSection_Product
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.WarehouseSection_Product.Include(w => w.Product).Include(w => w.WarehouseSection);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: WarehouseSection_Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WarehouseSection_Product == null)
            {
                return NotFound();
            }

            var warehouseSection_Product = await _context.WarehouseSection_Product
                .Include(w => w.Product)
                .Include(w => w.WarehouseSection)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (warehouseSection_Product == null)
            {
                return NotFound();
            }

            return View(warehouseSection_Product);
        }

        // GET: WarehouseSection_Product/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description");
            return View();
        }

        // POST: WarehouseSection_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,WarehouseSectionId,Quantity,ReservedQuantity")] WarehouseSection_Product warehouseSection_Product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouseSection_Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", warehouseSection_Product.ProductId);
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description", warehouseSection_Product.WarehouseSectionId);
            return View(warehouseSection_Product);
        }

        // GET: WarehouseSection_Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WarehouseSection_Product == null)
            {
                return NotFound();
            }

            var warehouseSection_Product = await _context.WarehouseSection_Product.FindAsync(id);
            if (warehouseSection_Product == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", warehouseSection_Product.ProductId);
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description", warehouseSection_Product.WarehouseSectionId);
            return View(warehouseSection_Product);
        }

        // POST: WarehouseSection_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,WarehouseSectionId,Quantity,ReservedQuantity")] WarehouseSection_Product warehouseSection_Product)
        {
            if (id != warehouseSection_Product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseSection_Product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseSection_ProductExists(warehouseSection_Product.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", warehouseSection_Product.ProductId);
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description", warehouseSection_Product.WarehouseSectionId);
            return View(warehouseSection_Product);
        }

        // GET: WarehouseSection_Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WarehouseSection_Product == null)
            {
                return NotFound();
            }

            var warehouseSection_Product = await _context.WarehouseSection_Product
                .Include(w => w.Product)
                .Include(w => w.WarehouseSection)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (warehouseSection_Product == null)
            {
                return NotFound();
            }

            return View(warehouseSection_Product);
        }

        // POST: WarehouseSection_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WarehouseSection_Product == null)
            {
                return Problem("Entity set 'SupermarketDbContext.WarehouseSection_Product'  is null.");
            }
            var warehouseSection_Product = await _context.WarehouseSection_Product.FindAsync(id);
            if (warehouseSection_Product != null)
            {
                _context.WarehouseSection_Product.Remove(warehouseSection_Product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseSection_ProductExists(int id)
        {
          return (_context.WarehouseSection_Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
