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
    public class MealCardsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public MealCardsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: MealCards
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Employee.Include(m => m.MealCard);
            return View(await supermarketDbContext.ToListAsync());
        }

        // GET: MealCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MealCard == null)
            {
                return NotFound();
            }

            var mealCard = await _context.MealCard
                .Include(m => m.Employee)
                .Include(m => m.CardMovements)
                .FirstOrDefaultAsync(m => m.MealCardId == id);
            if (mealCard == null)
            {
                return NotFound();
            }
            
            return View(mealCard);
        }

        // GET: MealCards/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: MealCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(int employeeId)
        {
            var employee = _context.Employee.Find(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            if (employee.MealCard == null)
            {
                var mealCard = new MealCard
                {
                    EmployeeId = employee.EmployeeId,
                    // Adicione outras propriedades conforme necessário
                };

                _context.Add(mealCard);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
