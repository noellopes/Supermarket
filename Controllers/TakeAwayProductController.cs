using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class TakeAwayProductController : Controller
    {
        private readonly SupermarketDbContext _context;

        public TakeAwayProductController(SupermarketDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = _context.TakeAwayProduct.Include("Category");
            return View(await products.ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<TakeAwayCategory>(), "CategoryId", "Name");
            return View();
        }

    }
}
