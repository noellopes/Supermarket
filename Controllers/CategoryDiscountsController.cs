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
    public class CategoryDiscountsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public CategoryDiscountsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: CategoryDiscounts
        public async Task<IActionResult> CategoryDiscountTopSeller()
        {
            // Definir o período desejado (por exemplo, 4 meses a partir da data atual)
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(4);

            // Obter os produtos mais vendidos no período desejado
            var topSellingProducts = await _context.Orders
                .Include(o => o.Product)
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();

            // Agrupar por CategoryId e calcular a quantidade total vendida localmente
            var categoryQuantities = topSellingProducts
                .GroupBy(o => o.Product.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    TotalQuantitySold = g.Sum(o => o.Quantity)
                });

            // Obter os ClientCardIds existentes
            var existingClientCardIds = await _context.ClientCard
                .Select(cc => cc.ClientCardId)
                .ToListAsync();

            // Loop para criar descontos para as categorias dos produtos mais vendidos
            foreach (var topProductGroup in topSellingProducts)
            {
                var productCategory = topProductGroup.ProductId;

                // Loop para criar descontos apenas para ClientCardIds existentes
                foreach (var clientCardId in existingClientCardIds)
                {
                    var categoryDiscount = new CategoryDiscounts
                    {
                        CategoryId = productCategory,
                        ClientCardId = clientCardId,
                        Value = 10, // Defina o valor do desconto conforme necessário
                        StartDate = startDate.AddDays(7),
                        EndDate = startDate.AddDays(14), // Ajuste conforme necessário
                    };

                    _context.Add(categoryDiscount);
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Seasonal Discounts Created with Success!";
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Index(int page = 1, string category = "", float? value = null, DateTime? startDate = null, DateTime? endDate = null)
        {
     
            //obter os descontos de categorias
            var categoryDiscounts = from b in _context.CategoryDiscounts.Include(b => b.ClientCard).Include(b => b.Category) select b;  
            //pesquisa dos descontos de categorias
            if (category != "")
            {
                categoryDiscounts = categoryDiscounts.Where(b => b.Category.Name.Contains(category));
            }
            if (value.HasValue)
            {
                categoryDiscounts = categoryDiscounts.Where(b => b.Value == value.Value);
            }
            if (startDate.HasValue)
            {
                categoryDiscounts = categoryDiscounts.Where(b => b.StartDate <= startDate.Value.Date && b.StartDate >= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                categoryDiscounts = categoryDiscounts.Where(b => b.EndDate <= endDate.Value.Date);
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await categoryDiscounts.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }
            //Ligação com o ViewModel para obter a categoria procurada
            var vm = new CategoryDiscountsViewModel
            {
                CategoryDiscounts = await categoryDiscounts
                    .OrderBy(b => b.Category.Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
            };
            return View(vm);
        }

        // GET: CategoryDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscounts = await _context.CategoryDiscounts
                .Include(c => c.Category)
                .Include(c => c.ClientCard)
                .FirstOrDefaultAsync(m => m.CategoryDiscountsId == id);
            if (categoryDiscounts == null)
            {
                return NotFound();
            }

            return View(categoryDiscounts);
        }

        // GET: CategoryDiscounts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardId");
            return View();
        }

        // POST: CategoryDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryDiscountsId,CategoryId,ClientCardId,Value,StartDate,EndDate")] CategoryDiscounts categoryDiscounts)
        {
            if (ModelState.IsValid)
            {
                //obter os cartões Ativos
                var clientCards = await _context.ClientCard.Where(b => b.Estado == true).ToListAsync();
                bool duplicatedDiscount = false; //detetar se os descontos são duplicados
                // Verificação que a data de início não seja anterior à data atual
                if (categoryDiscounts.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date must be equal to or later than today.");
                    duplicatedDiscount = true;
                }

                // Verificação que o valor do desconto seja maior que 0
                if (categoryDiscounts.Value <= 0)
                {
                    ModelState.AddModelError("Value", "Discount value must be greater than 0.");
                    duplicatedDiscount = true;
                }

                // Verificação que o valor do desconto seja menor do que 100
                if (categoryDiscounts.Value > 100)
                {
                    ModelState.AddModelError("Value", "Discount value must be lower than 100.");
                    duplicatedDiscount = true;
                }
                //Verificação descontos duplicados
                foreach (var clientCard in clientCards)
                {
                    bool discountExists = await _context.CategoryDiscounts.AnyAsync(
                        b => b.CategoryId == categoryDiscounts.CategoryId &&
                        b.ClientCardId == clientCard.ClientCardId &&
                        b.Value == categoryDiscounts.Value);

                    //se não houver adciona um novo desconto    
                    if (!discountExists)
                    {
                        var newCategoryDiscounts = new CategoryDiscounts
                        {
                            CategoryId = categoryDiscounts.CategoryId,
                            ClientCardId = clientCard.ClientCardId,
                            Value = categoryDiscounts.Value,
                            StartDate = categoryDiscounts.StartDate,
                            EndDate = categoryDiscounts.EndDate,
                        };

                        _context.Add(newCategoryDiscounts);
                    }
                    else
                    {
                        duplicatedDiscount = true;
                    }
                }
                //se houver exibe a mesnagem de erro a dizer descontos duplicados
                if (duplicatedDiscount)
                {
                    ModelState.AddModelError("", "One or more category Discounts with the same values already exist");
                    ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", categoryDiscounts.ClientCardId);
                    ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscounts.CategoryId);
                    return View(categoryDiscounts);
                }
                // guarda os descontos na base de dados e redireciona para Index
                else
                {
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "CategoryDiscount successfully created!";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", categoryDiscounts.ClientCardId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscounts.CategoryId);
            return View(categoryDiscounts);
        }

        // GET: CategoryDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscounts = await _context.CategoryDiscounts.FindAsync(id);
            if (categoryDiscounts == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscounts.CategoryId);
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardId", categoryDiscounts.ClientCardId);
            return View(categoryDiscounts);
        }

        // POST: CategoryDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryDiscountsId,CategoryId,ClientCardId,Value,StartDate,EndDate")] CategoryDiscounts categoryDiscounts)
        {
            if (id != categoryDiscounts.CategoryDiscountsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryDiscounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryDiscountsExists(categoryDiscounts.CategoryDiscountsId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", categoryDiscounts.CategoryId);
            ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardId", categoryDiscounts.ClientCardId);
            return View(categoryDiscounts);
        }

        // GET: CategoryDiscounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryDiscounts == null)
            {
                return NotFound();
            }

            var categoryDiscounts = await _context.CategoryDiscounts
                .Include(c => c.Category)
                .Include(c => c.ClientCard)
                .FirstOrDefaultAsync(m => m.CategoryDiscountsId == id);
            if (categoryDiscounts == null)
            {
                return NotFound();
            }

            return View(categoryDiscounts);
        }

        // POST: CategoryDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryDiscounts == null)
            {
                return Problem("Entity set 'SupermarketDbContext.CategoryDiscounts'  is null.");
            }
            var categoryDiscounts = await _context.CategoryDiscounts.FindAsync(id);
            if (categoryDiscounts != null)
            {
                _context.CategoryDiscounts.Remove(categoryDiscounts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDiscountsExists(int id)
        {
          return (_context.CategoryDiscounts?.Any(e => e.CategoryDiscountsId == id)).GetValueOrDefault();
        }
    }
}
