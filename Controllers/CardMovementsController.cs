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
    public class CardMovementsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public CardMovementsController(SupermarketDbContext context)
        {
            _context = context;
        }

        //// GET: CardMovements
        //public async Task<IActionResult> Index()
        //{
        //    var supermarketDbContext = _context.CardMovement.Include(c => c.MealCard);
        //    return View(await supermarketDbContext.ToListAsync());
        //}

        // GET: CardMovements/Details/5
        public async Task<IActionResult> Details(int cardMovementId)
        {
            var id = _context.CardMovement.Find(cardMovementId);
            if (id == null || _context.CardMovement == null)
            {
                return NotFound();
            }
            var cardMovement = await _context.CardMovement
                .Include(c => c.MealCard)
                .ThenInclude(mc => mc.Employee)
                .FirstOrDefaultAsync(m => m.CardMovementId == cardMovementId);
            if (cardMovement == null)
            {
                return NotFound();
            }

            return View(cardMovement);
        }

        // GET: CardMovements/Create
        [Authorize(Roles = "Cash Register")]
        public IActionResult Create(int mealCardId)
        {
            ViewData["MealCardId"] = new SelectList(_context.MealCard.Include(mc => mc.Employee), "MealCardId", "Employee.Employee_Name");
            ViewData["MCID"] = mealCardId;
            return View();
        }

        // POST: CardMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cash Register")]
        public async Task<IActionResult> Create([Bind("CardMovementId,Movement_Date,Value,Description,MealCardId")] CardMovement cardMovement)
        {
            var mealCard = await _context.MealCard.FindAsync(cardMovement.MealCardId);
            if (cardMovement.Movement_Date < DateTime.Now)
            {
                ModelState.AddModelError("Movement_Date", "Não pode registar movimentos no passado.");
                ViewData["MealCardId"] = new SelectList(_context.MealCard.Include(mc => mc.Employee), "MealCardId", "Employee.Employee_Name", cardMovement.MealCardId);
                return View(cardMovement);
            }
            if (cardMovement.Value < 0 && cardMovement.Value < -mealCard.Balance)
            {
                ModelState.AddModelError("Value", "Saldo insuficiente para a transação de débito.");
                ViewData["MealCardId"] = new SelectList(_context.MealCard.Include(mc => mc.Employee), "MealCardId", "Employee.Employee_Name", cardMovement.MealCardId);
                return View(cardMovement);
            }
            if (ModelState.IsValid)
            {

                if (cardMovement.Value < 0)
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
                return RedirectToAction("Details", "MealCards", new { id = cardMovement.MealCardId });
            }
            ViewData["MealCardId"] = new SelectList(_context.MealCard, "MealCardId", "MealCardId", cardMovement.MealCardId);
            return View(cardMovement);
        }

        public IActionResult back(int mc)
        {
            return RedirectToAction("Details", "MealCards", new { id = mc });
        }


    }
}
