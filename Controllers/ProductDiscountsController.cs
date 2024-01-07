using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Supermarket.Data;
using Supermarket.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Supermarket.Controllers
{
    public class ProductDiscountsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ProductDiscountsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ProductDiscounts/CreateBirthdayDiscount
        public IActionResult CreateBirthdayDiscount()
        {
            ViewData["ProductList"] = new SelectList(_context.Product, "ProductId", "Name");
            return View(new ProductDiscount());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateBirthdayDiscount(ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                var today = DateTime.Today;

                var clientsWithBirthday = _context.ClientCard
                    .Include(b => b.Client)
                    .Where(b => b.Client.ClientBirth.Month == today.Month && b.Client.ClientBirth.Day == today.Day)
                    .ToList();

                foreach (var clientCard in clientsWithBirthday)
                {
                    var newProductDiscount = new ProductDiscount
                    {
                        ProductId = productDiscount.ProductId,
                        ClientCardId = clientCard.ClientCardId,
                        Value = productDiscount.Value,
                        StartDate = DateTime.Today.AddDays(7),
                        EndDate = DateTime.Today.AddDays(14),
                    };

                    _context.Add(newProductDiscount);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Birthday Discount Created with Sucess!";
                return RedirectToAction(nameof(Index));
            }
                
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }
        // GET: ProductDiscounts
        public async Task<IActionResult> Index(int page = 1, string product = "", float? value = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var today = DateTime.Today;

            var clientsWithBirthday = _context.ClientCard
                .Include(b => b.Client)
                .Where(b => b.Client.ClientBirth.Month == today.Month && b.Client.ClientBirth.Day == today.Day)
                .ToList();

            var clientIdsWithBirthday = clientsWithBirthday.Select(b => b.ClientCardId).ToList();

            var productDiscounts = from b in _context.ProductDiscount.Include(b => b.ClientCard).Include(b => b.Product) select b; ;

            if (product != "")
            {
                productDiscounts = productDiscounts.Where(b => b.Product.Name.Contains(product));
            }
            if (value.HasValue)
            {
                productDiscounts = productDiscounts.Where(b => b.Value == value.Value);
            }
            if (startDate.HasValue)
            {
                productDiscounts = productDiscounts.Where(b => b.StartDate <= startDate.Value.Date && b.StartDate >= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                productDiscounts = productDiscounts.Where(b => b.EndDate <= endDate.Value.Date);
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await productDiscounts.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new ProductDiscountsViewModel
            {
                ProductDiscounts = await productDiscounts
                    .OrderBy(b => b.Product.Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                ClientsWithBirthday = clientsWithBirthday,
            };
            return View(vm);
        }

        // GET: ProductDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(b => b.ClientCard)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(b => b.ProductDiscountId == id);

            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // GET: ProductDiscounts/Create
        public IActionResult Create()
        {
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: ProductDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductDiscountId,ProductId,ClientCardId,Value,StartDate,EndDate")] ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                var clientCards = await _context.ClientCard.Where(b => b.Estado == true).ToListAsync();
                bool duplicatedDiscounts = false; //To detect if the discounts are duplicated

                foreach (var clientCard in clientCards)
                {
                    bool discountExistsForClient = await _context.ProductDiscount.AnyAsync(
                        b => b.ProductId == productDiscount.ProductId &&
                        b.ClientCardId == clientCard.ClientCardId &&
                        b.Value == productDiscount.Value);
                        
                    if (!discountExistsForClient)
                    {
                        var newProductDiscount = new ProductDiscount
                        {
                            ProductId = productDiscount.ProductId,
                            ClientCardId = clientCard.ClientCardId,
                            Value = productDiscount.Value,
                            StartDate = productDiscount.StartDate,
                            EndDate = productDiscount.EndDate,
                        };

                        _context.Add(newProductDiscount);
                    }
                    else
                    {
                        duplicatedDiscounts = true;
                    }
                }
                if (duplicatedDiscounts)
                {
                    ModelState.AddModelError("", "One or more product Discounts with the same values already exist for the same clients.");
                }
                else
                {
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product successfully created!";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", productDiscount.ClientCardId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount == null)
            {
                return NotFound();
            }
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", productDiscount.ClientCardId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // POST: ProductDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductDiscountId,ProductId,ClientCardId,Value,StartDate,EndDate")] ProductDiscount productDiscount)
        {
            if (id != productDiscount.ProductDiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDiscount);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "product sucessful edited!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDiscountExists(productDiscount.ProductDiscountId))
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
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", productDiscount.ClientCardId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productDiscount.ProductId);
            return View(productDiscount);
        }

        // GET: ProductDiscounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductDiscount == null)
            {
                return NotFound();
            }

            var productDiscount = await _context.ProductDiscount
                .Include(p => p.ClientCard)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductDiscountId == id);
            if (productDiscount == null)
            {
                return NotFound();
            }

            return View(productDiscount);
        }

        // POST: ProductDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductDiscount == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ProductDiscount'  is null.");
            }
            var productDiscount = await _context.ProductDiscount.FindAsync(id);
            if (productDiscount != null)
            {
                _context.ProductDiscount.Remove(productDiscount);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "product sucessful deleted";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDiscountExists(int id)
        {
            return (_context.ProductDiscount?.Any(e => e.ProductDiscountId == id)).GetValueOrDefault();
        }
    }
}
