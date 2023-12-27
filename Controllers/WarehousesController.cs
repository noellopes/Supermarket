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
    public class WarehousesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public WarehousesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
              return _context.Warehouse != null ? 
                          View(await _context.Warehouse.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Warehouse'  is null.");
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseId,Name,Adress")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                bool warehouseExists = await _context.Warehouse.AnyAsync(
              b => b.Name == warehouse.Name && b.Adress == warehouse.Adress);

                if (warehouseExists)
                {
                    ModelState.AddModelError("", "Another Warehouse with the same Name and Adress already exists.");
                }
                else
                {
                    _context.Add(warehouse);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Warehouse successfully created.";
                    return View("Details", warehouse);
                }
            }
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseId,Name,Adress")] Warehouse warehouse)
        {
            if (id != warehouse.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool WareHousesExists = await _context.Warehouse.AnyAsync(
                    b => b.Name == warehouse.Name && b.Adress == warehouse.Adress && b.WarehouseId != warehouse.WarehouseId);
                    if (WareHousesExists)
                    {
                        ModelState.AddModelError("", "Another Warehouse with the same Name and Adress already exists.");
                    }
                    else
                    {
                        _context.Update(warehouse);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Warehouse successfully edited.";
                        return View("Details", warehouse);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.WarehouseId))
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
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            var sectionsAssociatedWithWarehouse = await _context.WarehouseSection
            .Where(s => s.WarehouseId == id)
            .ToListAsync();

            if (sectionsAssociatedWithWarehouse.Count > 0)
            {
                ViewBag.ErrorMessage = "It is not possible to delete the warehouse as there are sections associated with it";
                ViewBag.SectionsAssociatedWithWarehouse = sectionsAssociatedWithWarehouse;
                return View("Delete");
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Warehouse == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Warehouse'  is null.");
            }
            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse != null)
            {
                _context.Warehouse.Remove(warehouse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult WarehouseProducts(int warehouseId)
        {
            var warehouseInfo = _context.Warehouse
                .Where(w => w.WarehouseId == warehouseId)
                .Select(w => new
                {
                    WarehouseName = w.Name
                })
                .FirstOrDefault();

            if (warehouseInfo == null)
            {
                return NotFound();
            }

            var warehouseProducts = _context.WarehouseSection_Product
            .Where(wp => wp.WarehouseSection.WarehouseId == warehouseId && wp.Product != null)
            .Include(wp => wp.Product)
            .ThenInclude(p => p.Brand)
            .AsEnumerable() 
            .GroupBy(wp => new
            {
                ProductId = wp.ProductId,
                Name = wp.Product.Name,
                Description = wp.Product.Description,
                Brand = wp.Product.Brand
            })
            .Select(group => new
            {
                ProductId = group.Key.ProductId,
                ProductName = group.Key.Name,
                ProductDescription = group.Key.Description,
                BrandName = group.Key.Brand != null ? group.Key.Brand.Name : "No Brand",
                Quantity = group.Sum(p => p.Quantity)
            })
            .ToList();




            ViewBag.WarehouseName = warehouseInfo.WarehouseName;
            ViewBag.TotalWarehouseProducts = warehouseProducts.Count;
            ViewBag.WarehouseProducts = warehouseProducts;

            return View();
        }

        private bool WarehouseExists(int id)
        {
          return (_context.Warehouse?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }
    }
}
