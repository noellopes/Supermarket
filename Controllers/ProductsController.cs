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
    public class ProductsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

             return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,BrandId,Name,Description,TotalQuantity,MinimumQuantity,UnitPrice,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                bool ProductExists = await _context.Product.AnyAsync(
                b => b.Name == product.Name && b.Description == product.Description);

                if (ProductExists)
                {
                    ModelState.AddModelError("", "Another Product with the same Name and Description already exists.");
                }
                else
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Product successfully created.";
                    return View("Details", product);
                }
            }
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,BrandId,Name,Description,TotalQuantity,MinimumQuantity,UnitPrice,Status")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Product.FindAsync(id);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    if (existingProduct.Name != product.Name || existingProduct.Description != product.Description)
                    {
                        bool productWithSameNameAndDescriptionExists = await _context.Product
                            .AnyAsync(p => p.ProductId != id && p.Name == product.Name && p.Description == product.Description);

                        if (productWithSameNameAndDescriptionExists)
                        {
                            ModelState.AddModelError("", "Another product with the same Name and Description already exists.");
                            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "Name", product.BrandId);
                            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
                            return View(product);
                        }
                    }
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.BrandId = product.BrandId;
                    existingProduct.TotalQuantity = product.TotalQuantity;
                    existingProduct.MinimumQuantity = product.MinimumQuantity;
                    existingProduct.UnitPrice = product.UnitPrice;
                    existingProduct.Status = product.Status;

                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "Product successfully edited.";
                    return View("Details", existingProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RotativeProducts(int warehouseSectionId = 0, int shelfId = 0)
        {
            // Consulta para as seções do armazém
            var warehouseSectionQuery = _context.WarehouseSection
                .Include(ws => ws.Warehouse)
                .Include(ws => ws.Products)
                .ThenInclude(wsp => wsp.Product.Category)
                .Include(ws => ws.Products)
                .ThenInclude(wsp => wsp.Product.Brand);

            // Executar a consulta e armazenar os resultados na ViewData
            var warehouseSections = await warehouseSectionQuery.ToListAsync();
            ViewData["WarehouseSections"] = warehouseSections;

            // Selecionar a seção do armazém com base no warehouseSectionId
            var selectedWarehouseSection = warehouseSections.FirstOrDefault(ws => ws.WarehouseSectionId == warehouseSectionId);
            ViewData["SelectedWarehouseSection"] = selectedWarehouseSection;

            // Consulta para as prateleiras
            var shelfQuery = _context.Shelf
                .Include(s => s.Hallway)
                .Include(s => s.Product)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Brand)
                .Include(s => s.Product)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.Hallway)
                .ThenInclude(h => h.Store);

            // Executar a consulta e armazenar os resultados na ViewData
            var shelves = await shelfQuery.ToListAsync();
            ViewData["Shelf"] = shelves;

            // Selecionar a prateleira com base no shelfId
            var selectedShelf = shelves.FirstOrDefault(s => s.ShelfId == shelfId);
            ViewData["SelectedShelf"] = selectedShelf;

            return View("RotativeInventory");
        }
        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
