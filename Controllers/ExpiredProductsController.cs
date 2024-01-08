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
    public class ExpiredProductsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ExpiredProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ExpiredProducts
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Index(int page = 1, string product = "", string barcode = "", string supplier = "", string employee = "")
        {

            var expiredproducts = from i in _context.ExpiredProducts
                    .Include(p => p.Product)
                    .Include(s => s.Supplier)
                    .Include(e => e.Employee)
                    .Include(pr => pr.Purchase)
            select i;

            if (product != "") expiredproducts = expiredproducts.Where(x => x.Product!.Name.Contains(product));

            if (barcode != "") expiredproducts = expiredproducts.Where(x => x.BarCode!.Contains(barcode));

            if (supplier != "") expiredproducts = expiredproducts.Where(x => x.Supplier!.Name.Contains(supplier));

            if (employee != "") expiredproducts = expiredproducts.Where(x => x.Employee!.Employee_Name.Contains(employee));

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = expiredproducts.Count()
            };

            return View(
                new ExpiredProductsListViewModel
                {
                    ExpiredProducts = expiredproducts.OrderByDescending(i => i.Product.Name).Skip((page - 1) * pagination.PageSize).Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchProduct = product,
                    SearchEmployee = employee,
                    SearchSupplier = supplier
                }
            );
            //return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ExpiredProducts/Details/5
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
                return NotFound();

            var expiredProducts = await _context.ExpiredProducts
                .Include(e => e.Employee)
                .Include(e => e.Product)
                .Include(e => e.Supplier)
                .FirstOrDefaultAsync(m => m.ExpiredProductId == id);

            if (expiredProducts == null)
                return View("ExpiredProductDeleted");

            return View(expiredProducts);
        }

        // GET: ExpiredProducts/Create
        [Authorize(Roles = "Create_Reports")]
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
        [Authorize(Roles = "Create_Reports")]
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
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
                return NotFound();

            var expiredProducts = await _context.ExpiredProducts.FindAsync(id);

            if (expiredProducts == null)
                return View("ExpiredProductDeleted");

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
        [Authorize(Roles = "Edit_Del_Reports")]
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
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpiredProducts == null)
                return NotFound();

            var expiredProducts = await _context.ExpiredProducts
                .Include(e => e.Employee)
                .Include(e => e.Product)
                .Include(e => e.Supplier)
                .FirstOrDefaultAsync(m => m.ExpiredProductId == id);

            if (expiredProducts == null)
                return View("ExpiredProductDeleted");

            return View(expiredProducts);
        }

        // POST: ExpiredProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpiredProducts == null)
                return Problem("Entity set 'SupermarketDbContext.ExpiredProducts'  is null.");
            
            var expiredProducts = await _context.ExpiredProducts
                .Include(p => p.Product)
                .Include(s => s.Supplier)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ExpiredProductId == id);

            if (expiredProducts != null)
                _context.ExpiredProducts.Remove(expiredProducts);
            
            await _context.SaveChangesAsync();

            return View("DeleteCompleted", expiredProducts);
        }

        private bool ExpiredProductsExists(int id)
        {
          return (_context.ExpiredProducts?.Any(e => e.ExpiredProductId == id)).GetValueOrDefault();
        }
    }
}
