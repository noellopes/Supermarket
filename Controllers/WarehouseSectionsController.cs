<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> FolgasPendentesAprovadas
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    [Authorize(Roles = "Stock Administrator, Stock Operator")]
    public class WarehouseSectionsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public WarehouseSectionsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: WarehouseSections
        public async Task<IActionResult> Index(int warehouseId, int page = 1, string descripition = "")
        {
            TempData["CancelWarehouseId"] = warehouseId;

            var warehouseSections =  _context.WarehouseSection
             .Where(h => h.WarehouseId == warehouseId);
           

            var WarehouseName = _context.Warehouse.Find(warehouseId)?.Name;

            if (descripition != "")
            {
                warehouseSections = warehouseSections.Where(x => x.Description.Contains(descripition));
            }
            
            
            ViewBag.WarehouseId = warehouseId;
            ViewBag.WarehouseName = WarehouseName;
            ViewBag.WarehouseSections = warehouseSections;
            


            var totalWarehouseSection = await warehouseSections.CountAsync();
            PagingInfoProduct paging = new PagingInfoProduct
            {
                CurrentPage = page,
                TotalItems = totalWarehouseSection,
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new WarehouseSectionViewModel
            {
                WarehouseSection = await warehouseSections
                    .OrderBy(b => b.Description)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfoProduct = paging,
                SearchDescription = descripition,
               
            };
            ViewBag.WarehouseSection = vm.WarehouseSection;
            ViewBag.TotalWarehouseSections = vm.PagingInfoProduct.TotalItems;

            return View(vm);
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

        [Authorize(Roles = "Stock Administrator")]
        // GET: WarehouseSections/Create
        public IActionResult Create(int? warehouseId)
        {
            if (warehouseId.HasValue)
            {
                ViewBag.ErrorMessage2 = TempData["ErrorMessage2"] as string;
                ViewBag.WarehouseId2 = warehouseId.Value;
                ViewBag.warehouseName = _context.Warehouse.Find(warehouseId.Value)?.Name;
                TempData["WarehouseId2"] = warehouseId;
            }
            else
                return NotFound();

            return View();
        }

        // POST: WarehouseSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Create([Bind("WarehouseSectionId,Description")] WarehouseSection warehouseSection)
        {
            warehouseSection.WarehouseId = (int)TempData["WarehouseId2"];

            if (ModelState.IsValid)
            {
                bool WarehouseSectionExists = await _context.WarehouseSection.AnyAsync(
                   b => b.Description == warehouseSection.Description && b.WarehouseId == warehouseSection.WarehouseId && b.WarehouseSectionId != warehouseSection.WarehouseSectionId);

                if (WarehouseSectionExists)
                {
                    TempData["ErrorMessage2"] = "Another Warehouse Section with the same Description and Warehouse already exists.";
                }
<<<<<<< HEAD
                else {
                    try
                    {
                        _context.Add(warehouseSection);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Warehouse Section successfully created.";
                        return RedirectToAction("Details", new { id = warehouseSection.WarehouseId, warehouseId = warehouseSection.WarehouseId });
                    }
                    catch (DbUpdateException)
                    {
                        TempData["ErrorMessage2"] = "DataBase conection Error ";
                       
                    }
=======
                else
                {
                    _context.Add(warehouseSection);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Warehouse Section successfully created.";
                    warehouseSection.Warehouse = await _context.Warehouse.FindAsync(warehouseSection.WarehouseId);

                    return View("Details", warehouseSection);
>>>>>>> FolgasPendentesAprovadas
                }
            }
            ViewData["WarehouseId"] = new SelectList(_context.Set<Warehouse>(), "WarehouseId", "Name", warehouseSection.WarehouseId);
            return RedirectToAction("Create", new { warehouseId = TempData["WarehouseId2"] });
        }

        // GET: WarehouseSections/Edit/5
        [Authorize(Roles = "Stock Administrator")]
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
            ViewData["WarehouseId"] = new SelectList(_context.Set<Warehouse>(), "WarehouseId", "Name", warehouseSection.WarehouseId);
            return View(warehouseSection);
        }

        // POST: WarehouseSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
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
                    b => b.Description == warehouseSection.Description && b.WarehouseId == warehouseSection.WarehouseId && b.WarehouseSectionId != warehouseSection.WarehouseSectionId);

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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "WarehouseId", "Name", warehouseSection.WarehouseId);
            return View(warehouseSection);
        }

        // GET: WarehouseSections/Delete/5
        [Authorize(Roles = "Stock Administrator")]
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
            var hasProductsAssociated = await _context.WarehouseSection_Product
            .AnyAsync(wp => wp.WarehouseSectionId == id);

            if (hasProductsAssociated)
            {
                ViewBag.ErrorMessage = "It is not possible to delete the warehouses  as there are products associated with it";
                ViewBag.hasProductsAssociated = await _context.WarehouseSection_Product
                    .Include(wp => wp.Product)
                    .Where(wp => wp.WarehouseSectionId == id)
                    .ToListAsync();

                return View("Delete", warehouseSection);
            }
            return View(warehouseSection);
        }


        // POST: WarehouseSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
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
                await _context.SaveChangesAsync();
            }
<<<<<<< HEAD
            
            return RedirectToAction("Index", "WarehouseSections", new { warehouseId =warehouseSection?.WarehouseId });
        }

        [Authorize(Roles = "Stock Administrator, Stock Operator")]
        public IActionResult WarehouseSectionProducts(int warehouseSectionId)
        {
            var sectionInfo = _context.WarehouseSection
                .Where(ws => ws.WarehouseSectionId == warehouseSectionId)
                .Select(ws => new
                {
                    SectionName = ws.Description,
                    WarehouseId = ws.WarehouseId
                })
                .FirstOrDefault();

            if (sectionInfo == null)
            {
                return NotFound();
            }

            var products = _context.WarehouseSection_Product
                .Where(wp => wp.WarehouseSectionId == warehouseSectionId && wp.Product.Name != null)
                .Include(wp => wp.Product)
                .ThenInclude(p => p.Brand)
                .GroupBy(wp => wp.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    ProductName = group.First().Product.Name,
                    ProductDescription = group.First().Product.Description,
                    BrandName = group.First().Product.Brand != null ? group.First().Product.Brand.Name : "No Brand",
                    Quantity = group.Sum(p => p.Quantity)
                })
                .ToList();

            ViewBag.WarehouseSectionId = warehouseSectionId;
            ViewBag.WarehouseId = sectionInfo.WarehouseId;
            ViewBag.SectionName = sectionInfo.SectionName;
            ViewBag.TotalProducts = products.Count;
            ViewBag.Products = products;

            return View();
=======

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
>>>>>>> FolgasPendentesAprovadas
        }

        private bool WarehouseSectionExists(int id)
        {
            return (_context.WarehouseSection?.Any(e => e.WarehouseSectionId == id)).GetValueOrDefault();
        }
    }
}
