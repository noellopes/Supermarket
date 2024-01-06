using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class TakeAwayProductController : Controller
    {
        private readonly SupermarketDbContext _context;
        private const string CacheProductKey = "basketCache";
        private readonly IMemoryCache _memoryCache;

        public TakeAwayProductController(SupermarketDbContext context, IMemoryCache cache)
        {
            _context = context;
            _memoryCache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var products = _context.TakeAwayProduct.Include("Category");
            return View(await products.ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<TakeAwayCategory>(), "Id", "Name");
            return View();
        }

        //guard clause
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductName","CategoryId","Quantity","Price","EstimatedPreparationTimeAsMinutes")] TakeAwayProduct takeAwayProduct)
        {
            bool isExist = _context.TakeAwayProduct.Any(x=> x.ProductName == takeAwayProduct.ProductName);
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<TakeAwayCategory>(), "Id", "Name",takeAwayProduct.CategoryId);
                return View(takeAwayProduct);
            }

            if (isExist)
            {
                ModelState.AddModelError("", "The product you added is already exist");
                ViewData["CategoryId"] = new SelectList(_context.Set<TakeAwayCategory>(), "Id", "Name", takeAwayProduct.CategoryId);
                return View(takeAwayProduct);
            }

            _context.TakeAwayProduct.Add(takeAwayProduct);
            await _context.SaveChangesAsync();
            ViewBag.Message = "The product was succesfully added";
            var data = _context.TakeAwayProduct
                        .Include("Category")
                        .FirstOrDefault(x => x.ProductName == takeAwayProduct.ProductName);
            return View("Details",data);
        }

        public IActionResult Details(int id)
        {
            var data = _context.TakeAwayProduct
                        .Include("Category")
                        .FirstOrDefault(x => x.Id == id);
            return View(data);
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.TakeAwayProduct == null)
            {
                return NotFound();
            }

            var takeAwayProduct = _context.TakeAwayProduct.Include("Category").FirstOrDefault(x=> x.Id == id);
            if (takeAwayProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<TakeAwayCategory>(), "Id", "Name", takeAwayProduct.CategoryId);
            return View(takeAwayProduct);
        }

        [HttpPost]
        public IActionResult Edit(int? id, [Bind("Id","ProductName","CategoryId","Quantity","Price","EstimatedPreparationTimeAsMinutes")] TakeAwayProduct takeAwayProduct)
        {

            if (!ModelState.IsValid)
            {
                return View(takeAwayProduct);
            }

            var data = _context.TakeAwayProduct.Update(takeAwayProduct);
            _context.SaveChanges();
            ViewBag.Message = "Product succesfully updated";
            return View("Details",_context.TakeAwayProduct.Include("Category").FirstOrDefault(x=> x.Id == id));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TakeAwayProduct == null)
            {
                return NotFound();
            }

            var product = await _context.TakeAwayProduct
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.TakeAwayProduct == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Product'  is null.");
            }
            var product = await _context.TakeAwayProduct.FindAsync(id);
            if (product != null)
            {
                _context.TakeAwayProduct.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> AddBasket(int id)
        {
            TakeAwayProduct newProduct = new TakeAwayProduct();
            var product = _context.TakeAwayProduct.Include("Category").AsNoTracking().FirstOrDefault(x=> x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            newProduct.Id = id;
            newProduct.ProductName = product.ProductName;
            newProduct.Category = product.Category;
            newProduct.Price = product.Price;
            newProduct.Quantity = product.Quantity - 1;
            newProduct.QuantityReserved = product.QuantityReserved == null ? 1 : product.QuantityReserved + 1;
            newProduct.EstimatedPreparationTimeAsMinutes = product.EstimatedPreparationTimeAsMinutes;

            var basket = _memoryCache.Get<Basket>(CacheProductKey) ?? new Models.Basket();

            if (basket.Products.Any(x=> x.ProductName == newProduct.ProductName))
            {
                var data = basket.Products.Find(x=> x.ProductName == newProduct.ProductName);
                basket.Products.Remove(data!);
            }

            basket.Products.Add(newProduct);
            basket.TotalProductQuantityInBasket = basket.Products.Sum(x => x.QuantityReserved);
            basket.EstimatedPreparationTimeAsMinutes = (int)basket.Products.Select(x => x.EstimatedPreparationTimeAsMinutes * x.QuantityReserved).Sum();
            basket.TotalPrice = (double) basket.Products.Select(x => x.Price * x.QuantityReserved).Sum();

            _memoryCache.Set(CacheProductKey, basket);
            _context.TakeAwayProduct.Update(newProduct);
            _context.SaveChanges();

            return View("Index", await _context.TakeAwayProduct.Include("Category").ToListAsync());
        }

        public IActionResult Basket()
        {
            var basket = _memoryCache.Get<Basket>(CacheProductKey) ?? new Models.Basket();
            return View("Basket",basket);
        }

    }
}
