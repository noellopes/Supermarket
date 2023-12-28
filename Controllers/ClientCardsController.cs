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
              return _context.ClientCard != null ? 
                          View(await _context.ClientCard.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.ClientCard'  is null.");
        }

        // GET: ClientCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientCard == null)
            {
                return NotFound();
            }

            var clientCard = await _context.ClientCard
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
            return View();
        }

        // POST: ClientCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientCardId,ClientCardNumber,Balance,Estado")] ClientCard clientCard)
        {
            if (ModelState.IsValid)
            {
                var random = new Random();
                bool isUnique = false;
                while (!isUnique) 
                {
                    clientCard.ClientCardNumber = random.Next(100000, 999999);
                    isUnique = !_context.ClientCard.Any(c => c.ClientCardNumber == clientCard.ClientCardNumber);
                }
                _context.Add(clientCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(clientCard);
        }

        // POST: ClientCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientCardId,ClientCardNumber,Balance,Estado")] ClientCard clientCard)
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
