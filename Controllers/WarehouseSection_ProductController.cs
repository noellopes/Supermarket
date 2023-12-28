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
        public async Task<IActionResult> Index(int warehouseSectionId)
        {
            var warehouseSections = await _context.WarehouseSection_Product
             .Include(wp => wp.Product)
             .Include(wp => wp.WarehouseSection)
             .Where(h => h.WarehouseSectionId == warehouseSectionId)
             .ToListAsync();

            var WarehouseSectionName = _context.WarehouseSection.Find(warehouseSectionId)?.Description;

            ViewBag.WarehouseSectionId = warehouseSectionId;
            ViewBag.WarehouseId = _context.WarehouseSection.Find(warehouseSectionId)?.WarehouseId;
            ViewBag.WarehouseSectionName = WarehouseSectionName;
            ViewBag.WarehouseSections = warehouseSections;
            ViewBag.TotalWarehouseSectionsProduts = warehouseSections.Count;

            return View(warehouseSections);
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
                .Include(w => w.Supplier)
                .Include(w => w.WarehouseSection)
                .FirstOrDefaultAsync(m => m.WarehouseSection_ProductId == id);
            if (warehouseSection_Product == null)
            {
                return NotFound();
            }

            return View(warehouseSection_Product);
        }

        // GET: WarehouseSection_Product/Create
        public IActionResult Create(int? warehouseSectionId)
        {

            if (warehouseSectionId.HasValue)
            {
                ViewBag.ErrorMessage2 = TempData["ErrorMessage2"] as string;
                ViewBag.WarehouseSectionId2 = warehouseSectionId.Value;
                ViewBag.warehouseSectionName = _context.WarehouseSection.Find(warehouseSectionId.Value)?.Description;
                TempData["WarehouseSectionId2"] = warehouseSectionId;
            }
            else
                return NotFound();

            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "Name");
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description");
            return View();
        }

        // POST: WarehouseSection_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseSection_ProductId,ProductId,Quantity,ReservedQuantity,BatchNumber,ExpirationDate,SupplierID")] WarehouseSection_Product warehouseSection_Product)
        {
            warehouseSection_Product.WarehouseSectionId = (int)TempData["WarehouseSectionId2"];


            if (ModelState.IsValid)
            {
                bool WarehouseSectionProductsExists = await _context.WarehouseSection_Product.AnyAsync(
                    b => b.BatchNumber == warehouseSection_Product.BatchNumber
                 && b.WarehouseSectionId == warehouseSection_Product.WarehouseSectionId
                 && b.ProductId == warehouseSection_Product.ProductId);

                if (WarehouseSectionProductsExists)
                {
                    TempData["ErrorMessage2"] = "Another Warehouse Section Product  with the same BatchNumber, Product and Warehouse Section already exists.";
                }
                else
                {
                    try
                    {
                        _context.Add(warehouseSection_Product);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Warehouse Section successfully created.";

                        return RedirectToAction("Details", new { id = warehouseSection_Product.WarehouseSection_ProductId });
                    }

                    catch (DbUpdateException)
                    {
                        TempData["ErrorMessage2"] = "DataBase conection Error ";

                    }


                }
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", warehouseSection_Product.ProductId);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "Name", warehouseSection_Product.SupplierID);
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description", warehouseSection_Product.WarehouseSectionId);
                return RedirectToAction("Create", new { warehouseSectionId = TempData["WarehouseSectionId2"] });
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
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "Name", warehouseSection_Product.SupplierID);
            ViewData["WarehouseSectionId"] = new SelectList(_context.WarehouseSection, "WarehouseSectionId", "Description", warehouseSection_Product.WarehouseSectionId);
            return View(warehouseSection_Product);
        }

        // POST: WarehouseSection_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseSection_ProductId,ProductId,WarehouseSectionId,Quantity,ReservedQuantity,BatchNumber,ExpirationDate,SupplierID")] WarehouseSection_Product warehouseSection_Product)
        {
            if (id != warehouseSection_Product.WarehouseSection_ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool WarehouseSectionProductsExists = await _context.WarehouseSection_Product.AnyAsync(
                   b => b.BatchNumber == warehouseSection_Product.BatchNumber
                && b.WarehouseSectionId == warehouseSection_Product.WarehouseSectionId
                && b.ProductId == warehouseSection_Product.ProductId && b.WarehouseSection_ProductId!= warehouseSection_Product.WarehouseSection_ProductId);

                if (WarehouseSectionProductsExists)
                {
                    ModelState.AddModelError("", "Another Warehouse Section Product  with the same BatchNumber, Product and Warehouse Section already exists.");
                }
                    try
                {
                    var existingWarehouseSection_Product = await _context.WarehouseSection_Product
                    .AsNoTracking()
                    .FirstOrDefaultAsync(wp => wp.WarehouseSection_ProductId == id);

                    warehouseSection_Product.Quantity = existingWarehouseSection_Product.Quantity;
                    warehouseSection_Product.ReservedQuantity = existingWarehouseSection_Product.ReservedQuantity;

                    _context.Update(warehouseSection_Product);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = " Warehouse Section Product successfully edited.";

                    warehouseSection_Product.WarehouseSection = await _context.WarehouseSection.FindAsync(warehouseSection_Product.WarehouseSectionId);
                    warehouseSection_Product = await _context.WarehouseSection_Product
                    .Include(w => w.Product)
                    .Include(w => w.Supplier)
                    .Include(w => w.WarehouseSection)
                    .FirstOrDefaultAsync(m => m.WarehouseSection_ProductId == id);

                    return View("Details", warehouseSection_Product);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseSection_ProductExists(warehouseSection_Product.WarehouseSection_ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", warehouseSection_Product.ProductId);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "Name", warehouseSection_Product.SupplierID);
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
                .Include(w => w.Supplier)
                .Include(w => w.WarehouseSection)
                .FirstOrDefaultAsync(m => m.WarehouseSection_ProductId == id);
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
          return (_context.WarehouseSection_Product?.Any(e => e.WarehouseSection_ProductId == id)).GetValueOrDefault();
        }
    }
}
