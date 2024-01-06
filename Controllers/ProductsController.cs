using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Supermarket.Data;
using Supermarket.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> Index(int page = 1, string name = "", int? productId = null, string status = "")
        {
            var supermarketDbContext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
            //return View(await supermarketDbContext.ToListAsync());

            var products = from b in _context.Product select b;

            if (name != "")
            {
                products = products.Where(x => x.Name.Contains(name));
            }

            if (productId.HasValue)
            {
                products = products.Where(x => x.ProductId == productId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                products = products.Where(x => x.Status == status);
            }

            PagingInfoProduct paging = new PagingInfoProduct
            {
                CurrentPage = page,
                TotalItems = await products.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new ProductViewModel
            {
                Product = await products
                    .OrderBy(b => b.Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfoProduct = paging,
                SearchName = name,
            };

            ViewBag.totalProduct = vm.PagingInfoProduct.TotalItems;

            return View(vm);
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

        [Authorize(Roles = "Stock Administrator")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,BrandId,Name,Description,TotalQuantity,MinimumQuantity,UnitPrice,Status,LastCountDate")] Product product)
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
            ViewData["Status"] = new SelectList(Product.StatusList, product.Status);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,BrandId,Name,Description,TotalQuantity,MinimumQuantity,UnitPrice,Status,LastCountDate")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ProductExists = await _context.Product.AnyAsync(
                    b => b.Name == product.Name && b.Description == product.Description && b.ProductId != product.ProductId);

                    if (ProductExists)
                    {
                        ModelState.AddModelError("", "Another Product with the same Name and Description already exists.");
                    }
                    else
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Product successfully edited.";
                        return View("Details", product);
                    }
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
            ViewData["Status"] = new SelectList(Product.StatusList, product.Status);
            return View(product);
        }

        // GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Product == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product
        //        .Include(p => p.Brand)
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Product == null)
        //    {
        //        return Problem("Entity set 'SupermarketDbContext.Product'  is null.");
        //    }
        //    var product = await _context.Product.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Product.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> RotativeInventoryCriteria(string criterial, string selectedDate = "", int? SelectedNumber=0,float? SelectedPrice=0)
        {
         

            var selectedCritial = criterial;

            ViewBag.selectedCritial = selectedCritial;

            if (selectedDate != "" || SelectedNumber > 0 || SelectedPrice > 0)
            {
                TempData["SelectedStringDate"] = selectedDate;
                TempData["SelectedNumber"] = SelectedNumber.ToString(); 
                TempData["SelectedPrice"] = SelectedPrice.ToString(); 

                return RedirectToAction("RotativeProducts", new
                {
                    
                    selectedStringDate = selectedDate,
                    selectedNumber = SelectedNumber,
                    selectedPrice = SelectedPrice
                });
            }

            return View();
        }
        public async Task<IActionResult> RotativeProducts(string? selectedStringDate, int? selectedNumber, float? selectedPrice, int selectedProductId = 0, bool isButtonClicked = false)
        {
            

            if (selectedStringDate != null && selectedNumber != null && selectedPrice != null)
            {
                var Date = selectedStringDate;
                var Number = selectedNumber;
                var Price = selectedPrice;

                TempData["SelectedStringDate"] = Date;
                TempData["SelectedNumber"] = int.Parse(Number.ToString());
                TempData["SelectedPrice"] = float.Parse(Price.ToString());

                var currentDate = DateTime.Now.Date;
                int days = 0;
                switch (Date)
                {
                    case "Month":
                        days = 31;
                        break;

                    case "2Week":
                        days = 14;
                        break;

                    case "week":
                        days = 7;
                        break;
                    case "2day":
                        days = 2;
                        break;
                }

                var expensiveProducts = await _context.Product
                    .Include(p => p.Brand)
                    .Where(p => p.UnitPrice > Price)
                    .ToListAsync();

                var mostCommonProducts = _context.ReduceProduct
         .Include(rp => rp.Product)
             .ThenInclude(p => p.Brand)
         .GroupBy(rp => rp.ProductId)
         .OrderByDescending(g => g.Count())
         .Take((int)Number)
         .Select(g => g.First().Product)
         .ToList();

                var filteredProducts = expensiveProducts
                    .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days)
                    .ToList();

                filteredProducts.AddRange(mostCommonProducts
                .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days)
                .Distinct()
            );

                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;
              


                if (isButtonClicked)
                {
                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    string description = "A new Rotative Inventory was been made for product:  " + selectedProduct.Name + " in" + selectedProduct.LastCountDate;
                    Alert alert = new Alert
                    {
                        Role = "Stock Operator",
                        Description = description,
                        Function="ReduceProducts"

                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();


                    return RedirectToAction("RotativeProducts", new
                    {
                        selectedProductId = 0,
                        selectedStringDate = Date,
                        selectedNumber = Number,
                        selectedPrice = Price
                    });


                }
            }
            else
            {
                var currentDate = DateTime.Now.Date;
                var expensiveProducts = await _context.Product
              .Include(p => p.Brand)
              .Where(p => p.UnitPrice > 25)
              .ToListAsync();

                var mostCommonProducts = _context.ReduceProduct
         .Include(rp => rp.Product)
             .ThenInclude(p => p.Brand)
         .GroupBy(rp => rp.ProductId)
         .OrderByDescending(g => g.Count())
         .Take(2)
         .Select(g => g.First().Product)
         .ToList();

                var filteredProducts = expensiveProducts
             .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= 2)
             .ToList();

                filteredProducts.AddRange(mostCommonProducts
        .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= 2));

                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;

               

                if (isButtonClicked)
                {
                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    string description = "A new Rotative Inventory was been made for product:  " + selectedProduct.Name + " in" + selectedProduct.LastCountDate;
                    Alert alert = new Alert
                    {
                        Role = "Stock Administration",
                        Description = description,
                        

                    };
                    _context.Add(alert);
                    

                    await _context.SaveChangesAsync();
                    return RedirectToAction("RotativeProducts", new { SelectedProductId = 0 });
                }

            }
            var warehouseSectionProducts = await _context.WarehouseSection_Product
                .Where(wp => wp.ProductId == selectedProductId)
                .Include(wp => wp.WarehouseSection)
                .ThenInclude(ws => ws.Warehouse)
                .ToListAsync();

            var selftProducts = await _context.Shelft_ProductExhibition
                .Where(wp => wp.ProductId == selectedProductId)
                .Include(wp => wp.Shelf)
                .ThenInclude(ws => ws.Hallway)
                .ThenInclude(ws => ws.Store)
                .ToListAsync();

            var selftProductsList = selftProducts.ToList();
            var warehouseSectionProductsList = warehouseSectionProducts.ToList();

            var totalWarehouseQuantity = warehouseSectionProductsList.Sum(wp => wp.Quantity);
            var totalShelfQuantity = selftProductsList.Sum(sp => sp.Quantity);
            var grandTotalQuantity = totalWarehouseQuantity + totalShelfQuantity;

            ViewData["TotalWarehouseQuantity"] = totalWarehouseQuantity;
            ViewData["TotalShelfQuantity"] = totalShelfQuantity;
            ViewData["GrandTotalQuantity"] = grandTotalQuantity;
            ViewData["WarehouseSectionProductsList"] = warehouseSectionProductsList;
            ViewData["SelftProductsList"] = selftProductsList;

            return View("RotativeInventory");

        }


        

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
