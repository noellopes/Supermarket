using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    [Authorize(Roles = "Stock Administrator, Stock Operator")]
    public class HallwaysController : Controller
    {
        private readonly SupermarketDbContext _context;

        public HallwaysController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Hallways
        public async Task<IActionResult> Index(int storeId)
        {
            var hallways = await _context.Hallway
             .Where(h => h.StoreId == storeId)
             .ToListAsync();

            var storeName = _context.Store.Find(storeId)?.Name;

            ViewBag.StoreId = storeId;
            ViewBag.StoreName = storeName;
            ViewBag.Hallways = hallways;
            ViewBag.TotalHallways = hallways.Count;

            return View(hallways);
        }

        // GET: Hallways/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway
                .Include(h => h.Store)
                .FirstOrDefaultAsync(m => m.HallwayId == id);
            if (hallway == null)
            {
                return NotFound();
            }

            return View(hallway);
        }

        // GET: Hallways/Create
        [Authorize(Roles = "Stock Administrator")]
        public IActionResult Create(int? storeId)
        {
            if (storeId.HasValue)
            {
                ViewBag.ErrorMessage2 = TempData["ErrorMessage2"] as string;
                ViewBag.StoreId2 = storeId.Value;
                ViewBag.StoreName = _context.Store.Find(storeId.Value)?.Name;
                TempData["StoreId2"] = storeId;
            }
            else
                return NotFound();

            return View();
        }

        // POST: Hallways/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Create([Bind("HallwayId,Description")] Hallway hallway)
        {
            hallway.StoreId = (int)TempData["StoreId2"];

            if (ModelState.IsValid)
            {
                bool HallwaysExists = await _context.Hallway.AnyAsync(
                    b => b.Description == hallway.Description && b.StoreId == hallway.StoreId);

                if (HallwaysExists)
                {
                    TempData["ErrorMessage2"] = "Another hallway with the same description and store already exists.";
                }
                else
                {
                    try { 
                    _context.Add(hallway);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Hallway successfully created.";
                    return RedirectToAction("Details", new { id = hallway.HallwayId, storeId = hallway.StoreId});
                    }
                    catch (DbUpdateException)
                    {

                        TempData["ErrorMessage2"] = "DataBase conection Error ";
                    }

                }
            }

            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return RedirectToAction("Create", new { storeId = TempData["StoreId2"] });
        }
        // GET: Hallways/Edit/5
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway.FindAsync(id);
            if (hallway == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return View(hallway);
        }

        // POST: Hallways/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("HallwayId,Description,StoreId")] Hallway hallway)
        {
            if (id != hallway.HallwayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    bool HallwaysExists = await _context.Hallway.AnyAsync(
                    b => b.Description == hallway.Description && b.StoreId == hallway.StoreId && b.HallwayId!=hallway.HallwayId);

                    if (HallwaysExists)
                    {
                        ModelState.AddModelError("", "Another Hallways with the same Description and Store already exists.");
                    }
                    else{
                        _context.Update(hallway);
                        await _context.SaveChangesAsync();

                        ViewBag.Message = "Hallways successfully edited.";
                       
                        hallway.Store = await _context.Store.FindAsync(hallway.StoreId);
                        return View("Details", hallway);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallwayExists(hallway.HallwayId))
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
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "StoreId", "Name", hallway.StoreId);
            return View(hallway);
        }
        // GET: Hallways/Delete/5
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hallway == null)
            {
                return NotFound();
            }

            var hallway = await _context.Hallway
                .Include(h => h.Store)
                .FirstOrDefaultAsync(m => m.HallwayId == id);

            if (hallway == null)
            {
                return NotFound();
            }

            var ShelfsAssociatedWithHallway = await _context.Shelf
                .Where(s => s.HallwayId == id)
                .ToListAsync();

            int storeIdToDelete = hallway.StoreId;

            if (ShelfsAssociatedWithHallway.Count > 0)
            {
                ViewBag.ErrorMessage = "It is not possible to delete the hallway as there are shelves associated with it";
                ViewBag.ShelfsAssociatedWithHallway = ShelfsAssociatedWithHallway;
                return View("Delete", hallway); 
            }

            return View(hallway);
        }

        // POST: Hallways/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hallway == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Hallway'  is null.");
            }

            var hallway = await _context.Hallway.FindAsync(id);

            if (hallway != null)
            {
                _context.Hallway.Remove(hallway);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Hallways", new { storeId = hallway?.StoreId });
        }

        public IActionResult HallwayProducts(int hallwayId)
        {
            var hallwayInfo = _context.Hallway
                .Where(h => h.HallwayId == hallwayId)
                .Select(h => new
                {
                    HallwayName = h.Description,
                     StoreId = h.StoreId
                })
                .FirstOrDefault();

            if (hallwayInfo == null)
            {
                return NotFound();
            }

            var products = _context.Shelft_ProductExhibition
                .Where(sp => sp.Shelf.HallwayId == hallwayId && sp.Product.Name != null)
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
            ViewBag.StoreId = hallwayInfo.StoreId;
            ViewBag.HallwayName = hallwayInfo.HallwayName;
            ViewBag.TotalProducts = products.Count;
            ViewBag.TotalQuantity = products.Sum(p => p.Quantity);
            ViewBag.Products = products;
          
          


            return View();
        }
        private bool HallwayExists(int id)
        {
          return (_context.Hallway?.Any(e => e.HallwayId == id)).GetValueOrDefault();
        }
    }
}
