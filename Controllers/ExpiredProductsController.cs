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
        public async Task<IActionResult> Index(int page = 1, string product = "", string batchnumber = "", DateTime? expirationdate = null)
        {

            var expiredproducts = from i in _context.ExpiredProducts
                    .Include(p => p.Product)
                    .Include(pr => pr.Purchase)
            select i;

            if (product != "") expiredproducts = expiredproducts.Where(x => x.Product!.Name.Contains(product));

            if (batchnumber != "") expiredproducts = expiredproducts.Where(x => x.BatchNumber!.Contains(batchnumber));

            if (expirationdate.HasValue) expiredproducts = expiredproducts.Where(x => x.ExpirationDate == expirationdate.Value.Date);

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = expiredproducts.Count()
            };

            return View(
                new ExpiredProductsListViewModel
                {
                    ExpiredProducts = expiredproducts.OrderByDescending(i => i.ExpirationDate).Skip((page - 1) * pagination.PageSize).Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchProduct = product,
                    SearchBatchNumber = batchnumber
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
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (expiredProducts == null)
                return View("ExpiredProductDeleted");

            return View(expiredProducts);
        }

        private bool ExpiredProductsExists(int id)
        {
          return (_context.ExpiredProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
