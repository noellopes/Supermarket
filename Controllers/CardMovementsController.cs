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
    public class CardMovementsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public CardMovementsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: CardMovements
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.CardMovement.Include(c => c.MealCard);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: CardMovements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CardMovement == null)
            {
                return NotFound();
            }

            var cardMovement = await _context.CardMovement
                .Include(c => c.MealCard)
                .FirstOrDefaultAsync(m => m.CardMovementId == id);
            if (cardMovement == null)
            {
                return NotFound();
            }

            return View(cardMovement);
        }

        // GET: CardMovements/Create
        public IActionResult Create()
        {
            ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId");
            return View();
        }

        // POST: CardMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardMovementId,Movement_Date,Value,Description,MealCardId")] CardMovement cardMovement)
        {
            var mealCard = await _context.MealCard.FindAsync(cardMovement.MealCardId);
            //if (cardMovement.Value < 0 && cardMovement.Value > mealCard.Balance)
            //{
            //    ModelState.AddModelError("Value", "Saldo insuficiente para a transação de débito.");
            //    ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId", cardMovement.MealCardId);
            //    return View(cardMovement);
            //}
            if (ModelState.IsValid)
            {
                
                if(cardMovement.Value < 0)
                {

                    cardMovement.Type = "Debit";
                    if (mealCard != null)
                    {
                        mealCard.Balance += cardMovement.Value;
                        _context.Update(mealCard);
                    }
                }
                else
                {
                        cardMovement.Type = "Credit";

                    
                    if (mealCard != null)
                    {
                        mealCard.Balance += cardMovement.Value;
                        _context.Update(mealCard);
                    }
                }
                _context.Add(cardMovement);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId", cardMovement.MealCardId);
            return View(cardMovement);
        }

        // GET: CardMovements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CardMovement == null)
            {
                return NotFound();
            }

            var cardMovement = await _context.CardMovement.FindAsync(id);
            if (cardMovement == null)
            {
                return NotFound();
            }
            ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId", cardMovement.MealCardId);
            return View(cardMovement);
        }

        // POST: CardMovements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardMovementId,Movement_Date,Value,Description,Type,MealCardId")] CardMovement cardMovement)
        {
            if (id != cardMovement.CardMovementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardMovement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardMovementExists(cardMovement.CardMovementId))
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
            ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId", cardMovement.MealCardId);
            return View(cardMovement);
        }

        // GET: CardMovements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CardMovement == null)
            {
                return NotFound();
            }

            var cardMovement = await _context.CardMovement
                .Include(c => c.MealCard)
                .FirstOrDefaultAsync(m => m.CardMovementId == id);
            if (cardMovement == null)
            {
                return NotFound();
            }

            return View(cardMovement);
        }

        // POST: CardMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CardMovement == null)
            {
                return Problem("Entity set 'SupermarketDbContext.CardMovement'  is null.");
            }
            var cardMovement = await _context.CardMovement.FindAsync(id);
            if (cardMovement != null)
            {
                _context.CardMovement.Remove(cardMovement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardMovementExists(int id)
        {
          return (_context.CardMovement?.Any(e => e.CardMovementId == id)).GetValueOrDefault();
        }
    }
}
