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
    public class ExpiredProductsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ExpiredProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ExpiredProducts
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ExpiredProducts.Include(e => e.Employee).Include(e => e.Product).Include(e => e.Supplier);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ExpiredProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
            {
                return NotFound();
            }

            var expiredProducts = await _context.ExpiredProducts
                .Include(e => e.Employee)
                .Include(e => e.Product)
                .Include(e => e.Supplier)
                .FirstOrDefaultAsync(m => m.ExpiredProductId == id);
            if (expiredProducts == null)
            {
                return NotFound();
            }

            return View(expiredProducts);
        }

        // GET: ExpiredProducts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: ExpiredProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpiredProductId,ProductId,FabricationDate,ExpirationDate,BarCode,SupplierId,EmployeeId")] ExpiredProducts expiredProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expiredProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", expiredProducts.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", expiredProducts.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", expiredProducts.SupplierId);
            return View(expiredProducts);
        }

        // GET: ExpiredProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
            {
                return NotFound();
            }

            var expiredProducts = await _context.ExpiredProducts.FindAsync(id);
            if (expiredProducts == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", expiredProducts.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", expiredProducts.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", expiredProducts.SupplierId);
            return View(expiredProducts);
        }

        // POST: ExpiredProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpiredProductId,ProductId,FabricationDate,ExpirationDate,BarCode,SupplierId,EmployeeId")] ExpiredProducts expiredProducts)
        {
            if (id != expiredProducts.ExpiredProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expiredProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpiredProductsExists(expiredProducts.ExpiredProductId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", expiredProducts.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", expiredProducts.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", expiredProducts.SupplierId);
            return View(expiredProducts);
        }

        // GET: ExpiredProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
            {
                return NotFound();
            }

            var expiredProducts = await _context.ExpiredProducts
                .Include(e => e.Employee)
                .Include(e => e.Product)
                .Include(e => e.Supplier)
                .FirstOrDefaultAsync(m => m.ExpiredProductId == id);
            if (expiredProducts == null)
            {
                return NotFound();
            }

            return View(expiredProducts);
        }

        // POST: ExpiredProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpiredProducts == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ExpiredProducts'  is null.");
            }
            var expiredProducts = await _context.ExpiredProducts.FindAsync(id);
            if (expiredProducts != null)
            {
                _context.ExpiredProducts.Remove(expiredProducts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpiredProductsExists(int id)
        {
          return (_context.ExpiredProducts?.Any(e => e.ExpiredProductId == id)).GetValueOrDefault();
        }
    }
}
