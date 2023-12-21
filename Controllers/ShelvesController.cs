﻿using System;
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
    public class ShelvesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ShelvesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Shelves
        public async Task<IActionResult> Index(int hallwaysId)
        {
            
            var hallways = await _context.Hallway.FindAsync(hallwaysId);

            if (hallways == null)
            {
                // Trate o cenário onde o Hallway não foi encontrado
                return NotFound();
            }

            var shelves = await _context.Shelf
                .Where(s => s.HallwayId == hallwaysId)
                .ToListAsync();

            var hallway = _context.Hallway
            .Include(h => h.Store) 
            .FirstOrDefault(h => h.HallwayId == hallwaysId);

            ViewBag.StoreId = hallway.StoreId;

            ViewBag.HallwaysId = hallwaysId;
            ViewBag.HallaysName = hallways.Description;
            ViewBag.Shelves = shelves;
            ViewBag.TotalShelft = shelves.Count();

            return View(shelves);


        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf
                .Include(s => s.Hallway)
                .FirstOrDefaultAsync(m => m.ShelfId == id);

            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        public IActionResult Create(int? hallwaysId)
        {
            if (hallwaysId.HasValue)
            {
                ViewBag.HallwaysId2 = hallwaysId.Value;
                ViewBag.HallwaysName = _context.Hallway.Find(hallwaysId.Value)?.Description;
            }

            ViewBag.HallwayIdOptions = new SelectList(_context.Hallway, "HallwayId", "Description");

            return View();
        }
        // POST: Shelves/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShelfId,Name,HallwayId")] Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                bool ShelvesExists = await _context.Shelf.AnyAsync(
                    b => b.Name == shelf.Name && b.HallwayId == shelf.HallwayId);

                if (ShelvesExists)
                {
                    ModelState.AddModelError("", "Another Shelf with the same Name and Hallway already exists.");
                }
                else
                {
                    _context.Add(shelf);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Hallway successfully created.";
                    shelf.Hallway = await _context.Hallway.FindAsync(shelf.HallwayId);
                    return RedirectToAction("Details", new { id = shelf.ShelfId, hallwayId = shelf.HallwayId });
                }
            }

            ViewBag.HallwayId = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }


        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShelfId,Name,HallwayId")] Shelf shelf)
        {
            if (id != shelf.ShelfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ShelvesExists = await _context.Shelf.AnyAsync(
                    b => b.Name == shelf.Name && b.HallwayId == shelf.HallwayId && b.ShelfId!=shelf.ShelfId);
                    if (ShelvesExists)
                    {
                        ModelState.AddModelError("", "Another Shelf with the same Name and Hallway already exists.");
                    }
                    else
                    {
                        _context.Update(shelf);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Shelf successfully edited.";
                        shelf.Hallway = await _context.Hallway.FindAsync(shelf.HallwayId);

                        return View("Details", shelf);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelfExists(shelf.ShelfId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
              //  return RedirectToAction(nameof(Index));
            }
            ViewData["HallwayId"] = new SelectList(_context.Hallway, "HallwayId", "Description", shelf.HallwayId);
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shelf == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf
                .Include(s => s.Hallway)
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (shelf == null)
            {
                return NotFound();
            }
            var hasProductsAssociated = await _context.Shelft_ProductExhibition
           .AnyAsync(wp => wp.ShelfId == id);

            if (hasProductsAssociated)
            {
                
                ViewBag.ErrorMessage = "It is not possible to delete the shelfts  as there are products associated with it";
                ViewBag.hasProductsAssociated = await _context.Shelft_ProductExhibition
                    .Include(wp => wp.Product)
                    .Where(wp => wp.ShelfId == id)
                    .ToListAsync();

                return View("Delete",shelf);
            }

            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shelf == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Shelf'  is null.");
            }
            var shelf = await _context.Shelf.FindAsync(id);
            if (shelf != null)
            {
                _context.Shelf.Remove(shelf);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Shelves", new { hallwayId = shelf?.HallwayId });

        }

        public IActionResult ShelfProducts(int shelfId)
        {
            var shelfInfo = _context.Shelf
                .Where(s => s.ShelfId == shelfId)
                .Select(s => new
                {
                    ShelfName = s.Name,
                    HallwayId = s.HallwayId
                })
                .FirstOrDefault();

            if (shelfInfo == null)
            {
                return NotFound();
            }

            var products = _context.Shelft_ProductExhibition
                .Where(sp => sp.ShelfId == shelfId && sp.Product.Name != null)
                .Include(sp => sp.Product)
                .ThenInclude(p => p.Brand)
                .GroupBy(sp => sp.ProductId) // Agrupar por ProductId
                .Select(group => new
                {
                    ProductName = group.First().Product.Name,
                    ProductDescription = group.First().Product.Description,
                    BrandName = group.First().Product.Brand != null ? group.First().Product.Brand.Name : "No Brand",
                    Quantity = group.Sum(p => p.Quantity)
                })
                .ToList();

            ViewBag.HallwayId = shelfInfo.HallwayId;
            ViewBag.ShelfName = shelfInfo.ShelfName;
            ViewBag.TotalProducts = products.Count;
            ViewBag.TotalQuantity = products.Sum(p => p.Quantity);
            ViewBag.Products = products;

            return View();
        }

        private bool ShelfExists(int id)
        {
          return (_context.Shelf?.Any(e => e.ShelfId == id)).GetValueOrDefault();
        }
    }
}
