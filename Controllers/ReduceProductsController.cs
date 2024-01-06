using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Supermarket.Controllers
{
    public class ReduceProductsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReduceProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ReduceProducts
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ReduceProduct.Include(r => r.Product).Include(r => r.Shelf).Include(r => r.WarehouseSection);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ReduceProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReduceProduct == null)
            {
                return NotFound();
            }

            var reduceProduct = await _context.ReduceProduct
                .Include(r => r.Product)
                .Include(r => r.Product!.Brand)
                .Include(r => r.Shelf)
                .Include(r => r.Shelf!.Hallway)
                .Include(r => r.Shelf!.Hallway!.Store)
                .Include(r => r.WarehouseSection)
                .Include(r => r.WarehouseSection!.Warehouse)
                .FirstOrDefaultAsync(m => m.ReduceProductId == id);
            if (reduceProduct == null)
            {
                return NotFound();
            }

            return View(reduceProduct);
        }

        [Authorize(Roles = "Stock Operator")]
        // GET: ReduceProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name");
            ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description");
            return View();
        }
        
        // POST: ReduceProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReduceProductId,Reason,Status,Date,Quantity,ProductId,WarehouseSectionId,ShelfId")] ReduceProduct reduceProduct)
        {
            var SectionProduct = await _context.WarehouseSection_Product
                   .FirstOrDefaultAsync(m => m.ProductId == reduceProduct.ProductId && m.WarehouseSectionId == reduceProduct.WarehouseSectionId);
            var ShelfProduct = await _context.Shelft_ProductExhibition
                   .FirstOrDefaultAsync(m => m.ProductId == reduceProduct.ProductId && m.ShelfId == reduceProduct.ShelfId);
            var product = await _context.Product.Where(a => a.ProductId == reduceProduct.ProductId).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                if (reduceProduct.WarehouseSectionId != null)
                {
                    if (SectionProduct == null)
                    {
                        ModelState.AddModelError(string.Empty, "The product does not exist in this section");
                    }
                    else
                    {
                        if (SectionProduct!.Quantity > 0)
                        {
                            if (reduceProduct.Quantity <= SectionProduct.Quantity)
                            {
                                _context.Add(reduceProduct);
                                await _context.SaveChangesAsync();
                                var str = new string("A new ReduceProduct has been created: " + product!.Name + ", " + reduceProduct.Quantity) + "chosed to be reduced";
                                Alert alert = new Alert
                                {
                                    Role = "Stock Administrator",
                                    Description = str,
                                    Function = "reduceProducts"
                                };
                                _context.Add(alert);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "More products to write-off than exist in the section");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "This section does not contain any more of this product to be writed-off, quantity = 0");
                        }
                    }
                }

                if (reduceProduct.ShelfId != null)
                {
                    if (ShelfProduct == null)
                    {
                        ModelState.AddModelError(string.Empty, "The product does not exist in this shelf");
                    }
                    else
                    {
                        if (ShelfProduct!.Quantity > 0)
                        {
                            if (reduceProduct.Quantity < ShelfProduct.Quantity)
                            {
                                _context.Add(reduceProduct);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "More products to write-off than exist in the shelf");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "This shelf does not contain any more of this product to be writed-off, quantity = 0");
                        }
                    }
                }
               
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", reduceProduct.ProductId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", reduceProduct.ShelfId);
            ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description", reduceProduct.WarehouseSectionId);
            return View(reduceProduct);
        }


        // GET: ReduceProducts/Edit/5 SelectProduct
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReduceProduct == null)
            {
                return NotFound();
            }

            //var reduceProduct = await _context.ReduceProduct.FindAsync(id);
            var reduceProduct = await _context.ReduceProduct
                .Include(r => r.Product)
                .Include(r => r.Product!.Brand)
                .Include(r => r.Shelf)
                .Include(r => r.Shelf!.Hallway)
                .Include(r => r.Shelf!.Hallway!.Store)
                .Include(r => r.WarehouseSection)
                .Include(r => r.WarehouseSection!.Warehouse)
                .FirstOrDefaultAsync(m => m.ReduceProductId == id);
            if (reduceProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", reduceProduct.ProductId);
            if (reduceProduct.WarehouseSectionId != null)
            {
                ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description", reduceProduct.WarehouseSectionId);
            }
            if (reduceProduct.ShelfId != null)
            {
                ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", reduceProduct.ShelfId);
            }
            
            ViewData["Status"] = new SelectList(ReduceProduct.StatusList, reduceProduct.Status);

            return View(reduceProduct);
        }

        // POST: ReduceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReduceProductId,Reason,Status,Date,Quantity,ProductId,WarehouseSectionId,ShelfId")] ReduceProduct reduceProduct)
        {
            if (id != reduceProduct.ReduceProductId)
            {
                return NotFound();
            }

            var SectionProduct = await _context.WarehouseSection_Product
                   .FirstOrDefaultAsync(m => m.ProductId == reduceProduct.ProductId && m.WarehouseSectionId == reduceProduct.WarehouseSectionId);
            var ShelfProduct = await _context.Shelft_ProductExhibition
                   .FirstOrDefaultAsync(m => m.ProductId == reduceProduct.ProductId && m.ShelfId == reduceProduct.ShelfId);

            //if(SectionProduct == null)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reduceProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReduceProductExists(reduceProduct.ReduceProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", reduceProduct.ProductId);
            if (reduceProduct.WarehouseSectionId != null)
            {
                ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description", reduceProduct.WarehouseSectionId);
            }
            if (reduceProduct.ShelfId != null)
            {
                ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", reduceProduct.ShelfId);
            }
            ViewData["Status"] = new SelectList(ReduceProduct.StatusList, reduceProduct.Status);
            return View(reduceProduct);
        }

        public async Task<IActionResult> ConfirmStatus(int? id)
        {
            if (id == null || _context.ReduceProduct == null)
            {
                return NotFound();
            }

            //var reduceProduct = await _context.ReduceProduct.FindAsync(id);
            var reduceProduct = await _context.ReduceProduct
                .Include(r => r.Product)
                .Include(r => r.Product!.Brand)
                .Include(r => r.Shelf)
                .Include(r => r.Shelf!.Hallway)
                .Include(r => r.Shelf!.Hallway!.Store)
                .Include(r => r.WarehouseSection)
                .Include(r => r.WarehouseSection!.Warehouse)
                .FirstOrDefaultAsync(m => m.ReduceProductId == id);
            if (reduceProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", reduceProduct.ProductId);
            if (reduceProduct.WarehouseSectionId != null)
            {
                ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description", reduceProduct.WarehouseSectionId);
            }
            if (reduceProduct.ShelfId != null)
            {
                ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", reduceProduct.ShelfId);
            }
            ViewData["Status"] = new SelectList(ReduceProduct.StatusList, reduceProduct.Status);
            return View(reduceProduct);
        }

        // POST: ReduceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmStatus(int id, [Bind("ReduceProductId,Reason,Status,Date,Quantity,ProductId,WarehouseSectionId,ShelfId")] ReduceProduct reduceProduct)
        {
            var productW = await _context.WarehouseSection_Product.Where(a => a.ProductId == reduceProduct.ProductId && a.WarehouseSectionId == reduceProduct.WarehouseSectionId && a.Quantity >= reduceProduct.Quantity).FirstOrDefaultAsync();
            var productS = await _context.Shelft_ProductExhibition.Where(a => a.ProductId == reduceProduct.ProductId && a.ShelfId == reduceProduct.ShelfId && a.Quantity >= reduceProduct.Quantity).FirstOrDefaultAsync();

            

            if (id != reduceProduct.ReduceProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reduceProduct);
                    await _context.SaveChangesAsync();
                    if (reduceProduct.Status == "Confirmed")
                    {
                        if (productW != null)
                        {
                            productW.Quantity -= reduceProduct.Quantity;
                            _context.Update(productW);
                            await _context.SaveChangesAsync();
                        }
                        if (productS != null)
                        {
                            productS.Quantity -= reduceProduct.Quantity;
                            _context.Update(productS);
                            await _context.SaveChangesAsync();
                        }
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReduceProductExists(reduceProduct.ReduceProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", reduceProduct.ProductId);
            if (reduceProduct.WarehouseSectionId != null)
            {
                ViewData["WarehouseSectionId"] = new SelectList(_context.Set<WarehouseSection>(), "WarehouseSectionId", "Description", reduceProduct.WarehouseSectionId);
            }
            if (reduceProduct.ShelfId != null)
            {
                ViewData["ShelfId"] = new SelectList(_context.Shelf, "ShelfId", "Name", reduceProduct.ShelfId);
            }
            ViewData["Status"] = new SelectList(ReduceProduct.StatusList, reduceProduct.Status);
            return View(reduceProduct);
        }

        // GET: ReduceProducts/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReduceProduct == null)
            {
                return NotFound();
            }

            var reduceProduct = await _context.ReduceProduct
                .Include(r => r.Product)
                .Include(r => r.Shelf)
                .Include(r => r.WarehouseSection)
                .FirstOrDefaultAsync(m => m.ReduceProductId == id);
            if (reduceProduct == null)
            {
                return NotFound();
            }

            return View(reduceProduct);
        }
            
        // POST: ReduceProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReduceProduct == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ReduceProduct'  is null.");
            }
            var reduceProduct = await _context.ReduceProduct.FindAsync(id);
            if (reduceProduct != null)
            {
                _context.ReduceProduct.Remove(reduceProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool ReduceProductExists(int id)
        {
          return (_context.ReduceProduct?.Any(e => e.ReduceProductId == id)).GetValueOrDefault();
        }
    }
}
