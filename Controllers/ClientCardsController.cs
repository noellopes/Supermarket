using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class ClientCardsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ClientCardsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ClientCards
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.ClientCard.Include(c => c.Client);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: ClientCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientCard == null)
            {
                return NotFound();
            }

            var clientCard = await _context.ClientCard
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ClientCardId == id);
            if (clientCard == null)
            {
                return NotFound();
            }

            return View(clientCard);
        }

        // GET: ClientCards/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName");
            return View();
        }

        // POST: ClientCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientCardId,ClientId,ClientCardNumber,Balance,Estado")] ClientCard clientCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName", clientCard.ClientId);
            return View(clientCard);
        }

        // GET: ClientCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientCard == null)
            {
                return NotFound();
            }

            var clientCard = await _context.ClientCard.FindAsync(id);
            if (clientCard == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName", clientCard.ClientId);
            return View(clientCard);
        }

        // POST: ClientCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientCardId,ClientId,ClientCardNumber,Balance,Estado")] ClientCard clientCard)
        {
            if (id != clientCard.ClientCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientCardExists(clientCard.ClientCardId))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName", clientCard.ClientId);
            return View(clientCard);
        }

        // GET: ClientCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientCard == null)
            {
                return NotFound();
            }

            var clientCard = await _context.ClientCard
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ClientCardId == id);
            if (clientCard == null)
            {
                return NotFound();
            }

            return View(clientCard);
        }

        // POST: ClientCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientCard == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ClientCard'  is null.");
            }
            var clientCard = await _context.ClientCard.FindAsync(id);
            if (clientCard != null)
            {
                _context.ClientCard.Remove(clientCard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientCardExists(int id)
        {
            return (_context.ClientCard?.Any(e => e.ClientCardId == id)).GetValueOrDefault();
        }
    }
}
