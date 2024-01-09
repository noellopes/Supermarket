using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

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
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateBirthdayDiscount()
        {
            ViewData["ProductList"] = new SelectList(_context.Product, "ProductId", "Name");
            return View(new ProductDiscount());
        }
        //Novo create para os desconto de aniversário
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task <IActionResult> CreateBirthdayDiscount(ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                //Verificar se um desconto com o mesmo nome e valor existe 
                bool discountExists = _context.ProductDiscount
                    .Any(pd => pd.ProductId == productDiscount.ProductId 
                    && pd.Value == productDiscount.Value);

                if (discountExists)
                {
                    ModelState.AddModelError("", "One or more product Discounts with the same values already exist for the same clients.");
                    return RedirectToAction(nameof(Index));
                }

                var today = DateTime.Today;
                //Ir buscar a lista do clientes que fazem anos associados ao cartão cliente
                var clientsWithBirthday = _context.ClientCard
                    .Include(b => b.Client)
                    .Where(b => b.Client.ClientBirth.Month == today.Month && b.Client.ClientBirth.Day == today.Day)
                    .ToList();
                // loop para criar o novo desconto de aniversário
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
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index(int page = 1, string product = "", float? value = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            //variavel para a data do aniversário
            var today = DateTime.Today;
            //obter uma lista de clientes que fazem aniversário
            var clientsWithBirthday = _context.ClientCard
                .Include(b => b.Client)
                .Where(b => b.Client.ClientBirth.Month == today.Month && b.Client.ClientBirth.Day == today.Day)
                .ToList();
            //Obter os ids do clientes que fazem anos
            var clientIdsWithBirthday = clientsWithBirthday.Select(b => b.ClientCardId).ToList();
            //obter os descontos de produtos
            var productDiscounts = from b in _context.ProductDiscount.Include(b => b.ClientCard).Include(b => b.Product) select b; ;
            //pesquisa dos descontos produto
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
            //Ligação com o ViewModel para obter o produto procurado
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
        [Authorize(Roles = "Client")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ProductDiscountId,ProductId,ClientCardId,Value,StartDate,EndDate")] ProductDiscount productDiscount)
        {
            if (ModelState.IsValid)
            {
                //obter os clientCards Ativos
                var clientCards = await _context.ClientCard.Where(b => b.Estado == true).ToListAsync();
                bool duplicatedDiscounts = false; //To detect if the discounts are duplicated
                // Verificação para garantir que a data de início não seja anterior à data atual
                if (productDiscount.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date must be equal to or later than today.");
                    duplicatedDiscounts = true;
                }

                // Verificação para garantir que o valor do desconto seja maior que 0
                if (productDiscount.Value <= 0)
                {
                    ModelState.AddModelError("Value", "Discount value must be greater than 0.");
                    duplicatedDiscounts = true;
                }

                // Verificação para garantir que o valor do desconto seja menor do que 100
                if (productDiscount.Value > 100)
                {
                    ModelState.AddModelError("Value", "Discount value must be lower than 100.");
                    duplicatedDiscounts = true;
                }
                //Verificação para saber se à descontos duplicados
                foreach (var clientCard in clientCards)
                {
                    bool discountExistsForClient = await _context.ProductDiscount.AnyAsync(
                        b => b.ProductId == productDiscount.ProductId &&
                        b.ClientCardId == clientCard.ClientCardId &&
                        b.Value == productDiscount.Value);

                    //se não houver adciona um novo desconto    
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
                //se houver exibe a mesnagem de erro a dizer descontos duplicados
                if (duplicatedDiscounts)
                {
                    ModelState.AddModelError("", "One or more product Discounts with the same values already exist for the same clients.");
                    ViewData["ClientCardId"] = new SelectList(_context.ClientCard, "ClientCardId", "ClientCardNumber", productDiscount.ClientCardId);
                    ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productDiscount.ProductId);
                    return View(productDiscount);
                }
                // salva os decontos na database e redereciona para o view Index
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
