using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
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
        public async Task<IActionResult> Index(int page = 1, string employee_name = "", bool sOEwithoutMC = false, bool sOEwithMC = false)
        {
            var employees = from b in _context.Employee.Include(m => m.MealCard) select b;

            if (!string.IsNullOrEmpty(employee_name))
            {
                employees = employees.Where(x => x.Employee_Name.Contains(employee_name));
            }

            if (sOEwithoutMC)
            {
                employees = employees.Where(x => x.MealCard == null);
            }

            if (sOEwithMC)
            {
                employees = employees.Where(x => x.MealCard != null);
            }
            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await employees.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new MealCardEmployeesViewModel
            {
                Employees = await employees
                    .OrderBy(b => b.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                MealCardPagingInfo = paging,
                SearchName = employee_name,
                SOEwithoutMC = sOEwithoutMC,
                SOEwithMC = sOEwithMC,
            };

            return View(vm);
        }

        // GET: MealCards/Details/5
        public async Task<IActionResult> Details(int? id, int cardMovementPage = 1)
        {
            if (id == null || _context.MealCard == null)
            {
                return NotFound();
            }

            var mealCard = await _context.MealCard
        .Include(m => m.Employee)
        .Include(m => m.CardMovements)
        .FirstOrDefaultAsync(m => m.MealCardId == id);

            var cardMovements = _context.CardMovement
                .Include(c => c.MealCard)
                .Where(c => c.MealCard.MealCardId == id);
            if (mealCard == null)
            {
                return NotFound();
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = cardMovementPage,
                TotalItems = await cardMovements.CountAsync(),
            };
            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }



            var vm = new MealCardEmployeesViewModel
            {
                Balance = mealCard.Balance,
                EmployeeName = mealCard.Employee.Employee_Name,
                MealCard = mealCard.MealCardId,
                CardMovements = await cardMovements
                .OrderBy(b => b.Movement_Date)
                .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                CardMovementPagingInfo = paging,
            };

            return View(vm);
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
                };

                _context.Add(mealCard);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult IndexTop()
        {
            return View();
        }
    }
}
