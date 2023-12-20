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
    public class AddressesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public AddressesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Addresses.Include(a => a.Supplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var addresses = await _context.Addresses
                .Include(a => a.Supplier)
                .FirstOrDefaultAsync(m => m.AddressesId == id);
            if (addresses == null)
            {
                return NotFound();
            }

            return View(addresses);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {

            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressesId,SupplierId,Address,Telephone")] Addresses addresses)
        {
            
                _context.Add(addresses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var addresses = await _context.Addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name", addresses.SupplierId);
            return View(addresses);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressesId,SupplierId,Address,Telephone")] Addresses addresses)
        {
            if (id != addresses.AddressesId)
            {
                return NotFound();
            }

           
            try
            {
                _context.Update(addresses);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(addresses.AddressesId))
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

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var addresses = await _context.Addresses
                .Include(a => a.Supplier)
                .FirstOrDefaultAsync(m => m.AddressesId == id);
            if (addresses == null)
            {
                return NotFound();
            }

            return View(addresses);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Addresses'  is null.");
            }
            var addresses = await _context.Addresses.FindAsync(id);
            if (addresses != null)
            {
                _context.Addresses.Remove(addresses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressesExists(int id)
        {
          return (_context.Addresses?.Any(e => e.AddressesId == id)).GetValueOrDefault();
        }
    }
}
