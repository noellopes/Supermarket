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
    public class StoresController : Controller
    {
        private readonly SupermarketDbContext _context;

        public StoresController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
              return _context.Store != null ? 
                          View(await _context.Store.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Store'  is null.");
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,Name,Adress")] Store store)
        {
            if (ModelState.IsValid)
            {
                bool StoreExists = await _context.Store.AnyAsync(
                b => b.Name == store.Name && b.Adress == store.Adress);

                if (StoreExists)
                {
                    ModelState.AddModelError("", "Another Store with the same Name and Adress already exists.");
                }
                else
                {
                    _context.Add(store);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Store successfully created.";
                    return View("Details", store);
                }
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,Name,Adress")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool StoreExists = await _context.Store.AnyAsync(
                    b => b.Name == store.Name && b.Adress == store.Adress&&b.StoreId!=store.StoreId);

                    if (StoreExists)
                    {
                     ModelState.AddModelError("", "Another Store with the same Name and Adress already exists.");
                    }
                    else
                    {
                        _context.Update(store);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Store successfully edited.";
                        return View("Details", store);
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }
            var hallwaysAssociatedWithStore = await _context.Hallway
            .Where(h => h.StoreId == id)
            .ToListAsync();

            if (hallwaysAssociatedWithStore.Count > 0)
            {
                ViewBag.ErrorMessage = "It is not possible to delete the store as there are hallways associated with it";
                ViewBag.AssociatedHallways = hallwaysAssociatedWithStore;
                return View("Delete");
            }

            

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Store == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Store'  is null.");
            }
            var store = await _context.Store.FindAsync(id);
            if (store != null)
            {
                _context.Store.Remove(store);
            }

            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult StoreProducts(int storeId)
        {   
             var storeInfo = _context.Store
            .Where(s => s.StoreId == storeId)
           .Select(s => new
            {
            StoreName = s.Name
            })
            .FirstOrDefault();

            if (storeInfo == null)
            {
                return NotFound(); 
            }

            var products = _context.Shelft_ProductExhibition
                .Where(sp => sp.Shelf.Hallway.StoreId == storeId && sp.Product.Name != null)
                .Include(sp => sp.Product)
                .GroupBy(sp => sp.ProductId) // Agrupar por ProductId
                .Select(group => new
                {
                    ProductName = group.First().Product.Name, // o nome do primeiro produto no grupo
                    Quantity = group.Sum(p => p.Quantity) // Somar a quantidade do grupo
                })
                .ToList();

            ViewBag.StoreName = storeInfo.StoreName;
            ViewBag.TotalProducts = products.Count;
            ViewBag.TotalQuantity = products.Sum(p => p.Quantity);
            ViewBag.Products = products;

            return View();
        }

        public IActionResult StoreHallways(int storeId)
        {
            var hallways = _context.Hallway
                .Where(h => h.StoreId == storeId)
                .ToList();

            ViewBag.StoreId = storeId;
            ViewBag.StoreName = _context.Store.Find(storeId)?.Name;
            ViewBag.Hallways = hallways;

            return View();
        }


        private bool StoreExists(int id)
        {
          return (_context.Store?.Any(e => e.StoreId == id)).GetValueOrDefault();
        }
    }
}
