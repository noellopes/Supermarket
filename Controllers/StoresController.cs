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
                    var existingStore = await _context.Store.FindAsync(id);

                    if (existingStore == null)
                    {
                        return NotFound();
                    }

                    if (existingStore.Name != store.Name || existingStore.Adress != store.Adress)
                    {
                        bool StoreWithSameNameAndAdressExists = await _context.Store
                            .AnyAsync(p => p.StoreId != id && p.Name == store.Name && p.Adress == store.Adress);

                        if (StoreWithSameNameAndAdressExists)
                        {
                            ModelState.AddModelError("", "Another Store with the same Name and Adress already exists.");
                            return View(store);
                        }
                    }
                    _context.Update(existingStore);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Store successfully edited.";
                    return View("Details", store);
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

        private bool StoreExists(int id)
        {
          return (_context.Store?.Any(e => e.StoreId == id)).GetValueOrDefault();
        }
    }
}
