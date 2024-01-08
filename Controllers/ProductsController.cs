using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Supermarket.Controllers
{
    [Authorize]
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
                    //.OrderBy(b => b.Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfoProduct = paging,
                SearchName = name,
            };

            //TotalQuantity
            //var totalProductsShelf = _context.Shelft_ProductExhibition
            //    .Where(p => p.Product.ProductId == p.ProductId)
            //    .GroupBy(p => p.ProductId) // Agrupar por ProductId
            //    .Select(group => new
            //    {
            //        Quantity = group.Sum(p => p.Quantity)
            //    });

            //var totalProductsWarehouse = _context.WarehouseSection_Product
            //    .Where(p => p.Product.ProductId == p.ProductId)
            //    .GroupBy(p => p.ProductId) // Agrupar por ProductId
            //    .Select(group => new
            //    {
            //        Quantity = group.Sum(p => p.Quantity)
            //    });

            //var Products = totalProductsShelf
            //    .Concat(totalProductsWarehouse)
            //    .GroupBy(p => p.Quantity)
            //    .Select(group => group.Key)
            //    .Sum();

            var totalProducts = _context.WarehouseSection_Product
                .Where(p => p.ProductId == null)
                .ToList();

            List<int> quantities = new List<int>();

            foreach (var item in products)
            {
                totalProducts = _context.WarehouseSection_Product
                .Where(p => p.ProductId == item.ProductId)
                //.OrderBy(p => p.Product.Name) 
                .ToList();

                int sum = 0;

                foreach (var item2 in totalProducts)
                {
                    sum += item2.Quantity;
                }
                
                quantities.Add(sum);
            }

         

            ViewBag.totalQuantity = quantities;
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

            var totalProducts = _context.WarehouseSection_Product
            .Where(p => p.ProductId == id)
            //.OrderBy(p => p.Product.Name) 
            .ToList();

            int sum = 0;

            foreach (var item in totalProducts)
            {
                sum += item.Quantity;
            }



            ViewBag.totalQuantity = sum;

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
        [Authorize(Roles = "Stock Administrator")]
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
        [Authorize(Roles = "Stock Administrator")]
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
        [Authorize(Roles = "Stock Administrator")]
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
        public async Task<IActionResult> RotativeInventoryCriteria(string criterial, string selectedDate = "", int? SelectedNumber = 0, float? SelectedPrice = 0)
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


        private Dictionary<int, int> warehouseSectionProductsDictionary = new Dictionary<int, int>();
        private Dictionary<int, int> selftProductsDictionary = new Dictionary<int, int>();
        private int Selectproduct;
        private int iniciate;
        private int iniciate2;

        [Authorize(Roles = "Stock Operator")]
        public async Task<IActionResult> RotativeProducts(string selectedStringDate = "", int? selectedNumber = 0, float? selectedPrice = 0, int selectedProductId = 0, bool isButtonClicked = false, int? quantityWarehouse = -1, int? IdWarehouseSectionProduct = 0, int? quantityShelves= -1, int? IdShelves = 0)
        {

            ViewData["SelectedStringDate"] = selectedStringDate;
            ViewData["SelectedNumber"] = selectedNumber;         
            ViewData["SelectedPrice"] = selectedPrice;
            if (selectedProductId > 0)
            {
                if (Selectproduct != selectedProductId)
                {
                    Selectproduct = selectedProductId;
                    iniciate = 0;
                    warehouseSectionProductsDictionary.Clear();
                }

                if (iniciate == 0)
                {


                    if (IdWarehouseSectionProduct > 0 && quantityWarehouse > -1)
                    {
                        iniciate = 1;

                        if (warehouseSectionProductsDictionary.ContainsKey(IdWarehouseSectionProduct.Value))
                        {
                            warehouseSectionProductsDictionary[IdWarehouseSectionProduct.Value] = quantityWarehouse.Value;
                        }
                        else
                        {
                            warehouseSectionProductsDictionary[IdWarehouseSectionProduct.Value] = quantityWarehouse.Value;
                        }
                    }
                }

            }

            foreach (var kvp in warehouseSectionProductsDictionary)
            {
                int warehouseSectionProductId = kvp.Key;
                int quantity = kvp.Value;

                
                var warehouseSectionProduct = await _context.WarehouseSection_Product.FindAsync(warehouseSectionProductId);

                if (warehouseSectionProduct != null)
                {
                    warehouseSectionProduct.Quantity = quantity;
                    await _context.SaveChangesAsync();
                }
            }


            
            if (selectedProductId > 0)
            {
                if (Selectproduct != selectedProductId)
                {
                    Selectproduct = selectedProductId;
                    iniciate2 = 0;
                    warehouseSectionProductsDictionary.Clear();
                }

                if (iniciate2 == 0)
                {


                    if (IdShelves > 0 && quantityShelves > -1)
                    {
                        iniciate2 = 1;

                        if (selftProductsDictionary.ContainsKey(IdShelves.Value))
                        {
                            selftProductsDictionary[IdShelves.Value] = quantityShelves.Value;
                        }
                        else
                        {
                            selftProductsDictionary[IdShelves.Value] = quantityShelves.Value;
                        }
                    }
                }

            }

            foreach (var kvp in selftProductsDictionary)
            {
                int idShelves = kvp.Key;
                int quantity = kvp.Value;

                var selves = await _context.Shelft_ProductExhibition
                .Where(sp => sp.ShelfId == idShelves && sp.ProductId == selectedProductId)
                .FirstOrDefaultAsync();

                if (selves != null)
                {
                    selves.Quantity = quantity;
                    await _context.SaveChangesAsync();
                }
            }

            // ViewData["WarehouseSectionProductsDictionary"] = warehouseSectionProductsDictionary;



            if (selectedStringDate != "" && selectedNumber>0 && selectedPrice >0)
            {

                var Date = selectedStringDate;
                var Number = selectedNumber;
                var Price = selectedPrice;


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
                    .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days&& p.UnitPrice > Price)
                    .ToList();

                var addedProductIds = new HashSet<int>(filteredProducts.Select(p => p.ProductId));

                filteredProducts.AddRange(mostCommonProducts
                    .Where(p => p.LastCountDate != null &&
                                (currentDate - p.LastCountDate.Date).TotalDays >= days &&
                                !addedProductIds.Contains(p.ProductId))
                    .Distinct());
            

                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;


                if (isButtonClicked)
                {
                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    var name = selectedProduct.Name;

                    // Save changes to the database
                    await _context.SaveChangesAsync();


                    var descripition = new string("A new Rotative Inventory was been: " + selectedProduct.Name + " in " + selectedProduct.LastCountDate);
                    Alert alert = new Alert
                    {
                        Role = "Stock Administrator",
                        Description = descripition,
                        Function = "Products",
                        Page = "Index",
                        Search = name
                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();

                    foreach (var kvp in warehouseSectionProductsDictionary)
                    {
                        int warehouseSectionProductId = kvp.Key;
                        int quantity = kvp.Value;

                        
                        var warehouseSectionProduct = await _context.WarehouseSection_Product.FindAsync(warehouseSectionProductId);

                        if (warehouseSectionProduct != null)
                        {
                            warehouseSectionProduct.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (var kvp in selftProductsDictionary)
                    {
                        int idShelves = kvp.Key;
                        int quantity = kvp.Value;


                        var selves = await _context.Shelft_ProductExhibition.FindAsync(idShelves, selectedProductId);

                        if (selves != null)
                        {
                            selves.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }

                    return RedirectToAction("RotativeProducts", new
                    {
                        SelectedProductId = 0,
                        selectedStringDate = Date,
                        selectedNumber = Number,
                        selectedPrice = Price
                    });


                }
            }
           else if (selectedStringDate != "" && selectedNumber > 0 && selectedPrice==0)
            {

                var Date = selectedStringDate;
                var Number = selectedNumber;
                var Price = selectedPrice;


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

             

                    var mostCommonProducts = _context.ReduceProduct
             .Include(rp => rp.Product)
                 .ThenInclude(p => p.Brand)
             .GroupBy(rp => rp.ProductId)
             .OrderByDescending(g => g.Count())
            .Take((int)Number)
             .Select(g => g.First().Product)
             .ToList();

                    var filteredProducts = mostCommonProducts
                        .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days)
                        .ToList();

                var addedProductIds = new HashSet<int>(filteredProducts.Select(p => p.ProductId));

         


                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;


                if (isButtonClicked)
                {
                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    var name = selectedProduct.Name;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    var descripition = new string("A new Rotative Inventory was been: " + selectedProduct.Name + " in " + selectedProduct.LastCountDate);
                    Alert alert = new Alert
                    {
                        Role = "Stock Administrator",
                        Description = descripition,
                        Function = "Products",
                        Page = "Index",
                        Search = name
                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();

                    foreach (var kvp in warehouseSectionProductsDictionary)
                    {
                        int warehouseSectionProductId = kvp.Key;
                        int quantity = kvp.Value;


                        var warehouseSectionProduct = await _context.WarehouseSection_Product.FindAsync(warehouseSectionProductId);

                        if (warehouseSectionProduct != null)
                        {
                            warehouseSectionProduct.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (var kvp in selftProductsDictionary)
                    {
                        int idShelves = kvp.Key;
                        int quantity = kvp.Value;


                        var selves = await _context.Shelft_ProductExhibition.FindAsync(idShelves, selectedProductId);

                        if (selves != null)
                        {
                            selves.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }


                    return RedirectToAction("RotativeProducts", new
                    {
                        SelectedProductId = 0,
                        selectedStringDate = Date,
                        selectedNumber = Number,
                        selectedPrice = Price
                    });


                }
            }
            else if (selectedStringDate != "" && selectedNumber == 0 && selectedPrice == 0)
            {

                var Date = selectedStringDate;
                var Number = selectedNumber;
                var Price = selectedPrice;


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
                 .ToListAsync();

                var filteredProducts = expensiveProducts
                    .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days)
                    .ToList();






                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;


                if (isButtonClicked)
                {

                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    var name = selectedProduct.Name;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    var descripition = new string("A new Rotative Inventory was been: " + selectedProduct.Name + " in " + selectedProduct.LastCountDate);
                    Alert alert = new Alert
                    {
                        Role = "Stock Administrator",
                        Description = descripition,
                        Function = "Products",
                        Page = "Index",
                        Search = name
                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();

                    foreach (var kvp in warehouseSectionProductsDictionary)
                    {
                        int warehouseSectionProductId = kvp.Key;
                        int quantity = kvp.Value;


                        var warehouseSectionProduct = await _context.WarehouseSection_Product.FindAsync(warehouseSectionProductId);

                        if (warehouseSectionProduct != null)
                        {
                            warehouseSectionProduct.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (var kvp in selftProductsDictionary)
                    {
                        int idShelves = kvp.Key;
                        int quantity = kvp.Value;


                        var selves = await _context.Shelft_ProductExhibition.FindAsync(idShelves, selectedProductId);

                        if (selves != null)
                        {
                            selves.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }

                    return RedirectToAction("RotativeProducts", new
                    {
                        SelectedProductId = 0,
                        selectedStringDate = Date,
                        selectedNumber = Number,
                        selectedPrice = Price
                    });


                }
            }
            else if (selectedStringDate != "" && selectedNumber ==0 && selectedPrice > 0)
            {

                var Date = selectedStringDate;
             
                var Price = selectedPrice;


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

              

                var filteredProducts = expensiveProducts
                    .Where(p => p.LastCountDate != null && (currentDate - p.LastCountDate.Date).TotalDays >= days)
                    .ToList();

              

                var selectedProduct = filteredProducts.FirstOrDefault(p => p.ProductId == selectedProductId);

                ViewData["Products"] = filteredProducts;
                ViewData["SelectedProduct"] = selectedProduct;


                if (isButtonClicked)
                {
                    // Update LastCountDate
                    selectedProduct.LastCountDate = DateTime.Now;

                    var name = selectedProduct.Name;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    var descripition = new string("A new Rotative Inventory was been: " + selectedProduct.Name + " in " + selectedProduct.LastCountDate);
                    Alert alert = new Alert
                    {
                        Role = "Stock Administrator",
                        Description = descripition,
                        Function = "Products",
                        Page="Index",
                        Search= name
                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();

                    foreach (var kvp in warehouseSectionProductsDictionary)
                    {
                        int warehouseSectionProductId = kvp.Key;
                        int quantity = kvp.Value;


                        var warehouseSectionProduct = await _context.WarehouseSection_Product.FindAsync(warehouseSectionProductId);

                        if (warehouseSectionProduct != null)
                        {
                            warehouseSectionProduct.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }
                    foreach (var kvp in selftProductsDictionary)
                    {
                        int idShelves = kvp.Key;
                        int quantity = kvp.Value;


                        var selves = await _context.Shelft_ProductExhibition.FindAsync(idShelves, selectedProductId);

                        if (selves != null)
                        {
                            selves.Quantity = quantity;
                            await _context.SaveChangesAsync();
                        }
                    }
                    return RedirectToAction("RotativeProducts", new
                    {
                        SelectedProductId = 0,
                        selectedStringDate = Date,
                        selectedPrice = Price
                    });


                }
            }
            else
            {

                ViewData["Products"] = new List<Product>();
                ViewData["SelectedProduct"] = null;
                ViewData["NoCriterial"] = true;
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

        [Authorize(Roles = "Stock Operator")]
        public async Task<IActionResult> RestoreExposure()
        {
            var productsToRestore = await _context.Shelft_ProductExhibition
                .Include(sp => sp.Product)
                .Where(sp => sp.Quantity < sp.MinimumQuantity)
                .Include(wp => wp.Shelf)
                .ToListAsync();

            List<WarehouseSection_Product> productsToGet = await _context.WarehouseSection_Product
                .Include(wp => wp.Product)
                .Include(wp => wp.WarehouseSection)
                .Where(wp => wp.Product == null)
                .ToListAsync();

            foreach (var item in productsToRestore)
            {
                productsToGet = await _context.WarehouseSection_Product
                    .Include(wp => wp.Product)
                    .Where(wp => wp.ProductId == item.ProductId && wp.Quantity > 0)
                    //.Where(wp => wp.ProductId == item.ProductId && wp.Quantity > 0 && wp.ExpirationDate > DateTime.Now)
                    .OrderBy(wp => wp.ExpirationDate)
                    .Include(wp => wp.WarehouseSection)
                    .ToListAsync();

                //var num = productsToRestore.Where(wp => wp.ProductId == item.ProductId && wp.Quantity > 0).FirstOrDefault().Quantity;

                //productsToGet.Where(wp => wp.ProductId == item.ProductId && wp.Quantity > 0).FirstOrDefault()!.ReservedQuantity = item.MinimumQuantity * 2 - num;
            }
            //var productsToGet = await _context.WarehouseSection_Product
            //    .Include(wp => wp.Product)
            //    .Where(wp => wp.ProductId == productsToRestore.Pr && wp.Quantity > 0)
            //    .OrderBy(wp => wp.ExpirationDate)
            //    .Include(wp => wp.WarehouseSection)
            //    .ThenInclude(wp => wp.Warehouse)
            //    .ToListAsync();


            ViewData["ProductsToRestore"] = productsToRestore.ToList();
            ViewData["ProductsToGet"] = productsToGet.ToList();

            return View("RestoreExposure");
        }




        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
