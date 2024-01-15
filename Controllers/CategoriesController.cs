using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    [Authorize(Roles = "Stock Administrator, Stock Operator")]
    public class CategoriesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int page = 1, string name = "")
        {

            var categories = _context.Category.AsQueryable();

            if (name != "")
            {
                categories = categories.Where(x => x.Name.Contains(name));
            }

            PagingInfoProduct paging = new PagingInfoProduct
            {
                CurrentPage = page,
                TotalItems = await categories.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new CategoriesViewModel
            {
                Categories = await categories
                    .OrderBy(b => b.Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfoProduct = paging,
                SearchName = name,
            };

            ViewBag.totalCategories = vm.PagingInfoProduct.TotalItems;

            return View(vm);
            //var categories = from b in _context.Category select b;

            //if (name != "")
            //{
            //    categories = categories.Where(x => x.Name.Contains(name));
            //}

            //PagingInfoProduct paging = new PagingInfoProduct
            //{
            //    CurrentPage = page,
            //    TotalItems = await categories.CountAsync(),
            //};

            //if (paging.CurrentPage <= 1)
            //{
            //    paging.CurrentPage = 1;
            //}
            //else if (paging.CurrentPage > paging.TotalPages)
            //{
            //    paging.CurrentPage = paging.TotalPages;
            //}

            //var vm = new CategoriesViewModel
            //{
            //    Categories = await categories
            //        .OrderBy(b => b.Name)
            //        .Skip((paging.CurrentPage - 1) * paging.PageSize)
            //        .Take(paging.PageSize)
            //        .ToListAsync(),
            //    PagingInfoProduct = paging,
            //    SearchName = name,
            //};

            ////var totalCategories = await categories.CountAsync()
            //ViewBag.totalCategories = vm.PagingInfoProduct.TotalItems;

            //return View(vm);
            //return _context.Category != null ? 
            //              View(await _context.Category.OrderBy(c => c.Name).ToListAsync()) :
            //              Problem("Entity set 'SupermarketDbContext.Category'  is null.");

            return _context.Category != null ?
                        View(await _context.Category.ToListAsync()) :
                        Problem("Entity set 'SupermarketDbContext.Category'  is null.");

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Stock Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Stock Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Category'  is null.");
            }
            var category = await _context.Category.FindAsync(id);
            var product = await _context.Product.Where(p => p.BrandId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                ModelState.AddModelError(string.Empty, "There is a product with this category, can't be deleted.");
                return View(category);
            }
            else if (category != null)
            {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Category?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
