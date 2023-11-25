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
    public class WarehouseSectionsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public WarehouseSectionsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: WarehouseSections
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.WarehouseSection.Include(w => w.Warehouse);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: WarehouseSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WarehouseSection == null)
            {
                return NotFound();
            }

            var warehouseSection = await _context.WarehouseSection
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.WarehouseSectionId == id);
            if (warehouseSection == null)
            {
                return NotFound();
            }

            return View(warehouseSection);
        }

        // GET: WarehouseSections/Create
        public IActionResult Create()
        {
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "WarehouseId", "Adress");
            return View();
        }

        // POST: WarehouseSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseSectionId,Description,WarehouseId")] WarehouseSection warehouseSection)
        {
            if (ModelState.IsValid)
            {
                bool WarehouseSectionExists = await _context.WarehouseSection.AnyAsync(
                   b => b.Description == warehouseSection.Description && b.WarehouseId == warehouseSection.WarehouseId);

                if (WarehouseSectionExists)
                {
                    ModelState.AddModelError("", "Another Warehouse Section with the same Description and Warehouse already exists.");
                }
                else { 
                    _context.Add(warehouseSection);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Warehouse Section successfully created.";
                    warehouseSection.Warehouse = await _context.Warehouse.FindAsync(warehouseSection.WarehouseId);

                    return View("Details", warehouseSection);
                }
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "WarehouseId", "Adress", warehouseSection.WarehouseId);
            return View(warehouseSection);
        }

        // GET: WarehouseSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WarehouseSection == null)
            {
                return NotFound();
            }

            var warehouseSection = await _context.WarehouseSection.FindAsync(id);
            if (warehouseSection == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "WarehouseId", "Adress", warehouseSection.WarehouseId);
            return View(warehouseSection);
        }

        // POST: WarehouseSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseSectionId,Description,WarehouseId")] WarehouseSection warehouseSection)
        {
            if (id != warehouseSection.WarehouseSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool WarehouseSectionExists = await _context.WarehouseSection.AnyAsync(
                    b => b.Description == warehouseSection.Description && b.WarehouseId == warehouseSection.WarehouseId);

                    if (WarehouseSectionExists)
                    {
                        ModelState.AddModelError("", "Another Warehouse Section with the same Description and Warehouse already exists.");
                    }
                    else
                    {
                        _context.Update(warehouseSection);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = " Warehouse Section successfully edited.";
                        warehouseSection.Warehouse = await _context.Warehouse.FindAsync(warehouseSection.WarehouseId);
                        return View("Details", warehouseSection);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseSectionExists(warehouseSection.WarehouseSectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
                //return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "WarehouseId", "Adress", warehouseSection.WarehouseId);
            return View(warehouseSection);
        }

        // GET: WarehouseSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WarehouseSection == null)
            {
                return NotFound();
            }

            var warehouseSection = await _context.WarehouseSection
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.WarehouseSectionId == id);
            if (warehouseSection == null)
            {
                return NotFound();
            }

            return View(warehouseSection);
        }

        // POST: WarehouseSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WarehouseSection == null)
            {
                return Problem("Entity set 'SupermarketDbContext.WarehouseSection'  is null.");
            }
            var warehouseSection = await _context.WarehouseSection.FindAsync(id);
            if (warehouseSection != null)
            {
                _context.WarehouseSection.Remove(warehouseSection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseSectionExists(int id)
        {
          return (_context.WarehouseSection?.Any(e => e.WarehouseSectionId == id)).GetValueOrDefault();
        }
    }
}
